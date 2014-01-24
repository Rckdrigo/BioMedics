using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public bool isServer;
	private bool refreshingHosts;
	private bool serverRegistered;

	private HostData[] hostData;

	// Use this for initialization
	void Start () {
		refreshingHosts = false;
		if(isServer){
			Network.InitializeServer(1,25000,!Network.HavePublicAddress());
			MasterServer.RegisterHost("SODVI","SODVI_Biomedics");
		}
	}

	void Update(){
		if(refreshingHosts){
			if(MasterServer.PollHostList().Length >0){
				refreshingHosts = false;
				hostData = MasterServer.PollHostList();
				print (hostData[0].gameName);
				Network.Connect(hostData[0].ip,25000);
			}
		}
	}

	// Update is called once per frame
	void OnServerInitialized () {
		print ("Servidor Iniciado");
	}	

	void OnMasterServerEvent(MasterServerEvent mse){
		if(mse == MasterServerEvent.RegistrationSucceeded){
			print ("Servidor Registrado");
			serverRegistered = true;
		}
	}

	void OnGUI(){
		if(!isServer){
			if(GUILayout.Button("Refresh Hosts")){
				refreshingHosts = true;
				MasterServer.RequestHostList("SODVI");
			}
		}else{
			GUILayout.Label("Server Registered: " + serverRegistered);
		}
	}


}
