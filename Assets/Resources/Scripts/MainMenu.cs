using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class MainMenu : MonoBehaviour {

	public GUISkin skin;
	public int port = 25000;

#if UNITY_ANDROID
	
	private bool refreshingHosts;

	int option = 0;
	string[] a = {"Izquierdo","Derecho"};

	string code = "";

	void Update(){

		if(refreshingHosts){
			HostData[] hostData;

			if(MasterServer.PollHostList().Length >0){
				refreshingHosts = false;
				hostData = MasterServer.PollHostList();

				if(hostData.Length > 0){
					string temp = "SODVI_"+code;
					for(int i = 0; i < hostData.Length; i++){
						if(hostData[i].gameName == temp){
							Network.Connect(hostData[i].ip,port);
							break;
						}
					}
				}
			}
		}
		

		if(Network.isClient){
			if(option == 0)
				PhoneInterface.rightHanded = false;
			else
				PhoneInterface.rightHanded = true;
		}
	}

	void OnGUI(){
		GUI.skin = skin;
		GUILayout.BeginArea(new Rect(0,0,Screen.width,Screen.height));
	
		GUILayout.Label("Reh-app-ilitacion");
		code = GUILayout.TextField(code);
		if(!Network.isClient){
			if(GUILayout.Button("Sincronizar")){
				refreshingHosts = true;
				MasterServer.RequestHostList("SODVI");
		}
		}else{
			GUILayout.Label("Sincronizado ");
			GUILayout.Label("Indica con que lado trabajaras en esta sesion: ");
			option = GUILayout.SelectionGrid(option,a,2);
		}

		
		GUILayout.EndArea();
	}
#else
	string randomCode;
	bool master;

	void Start(){
		randomCode = RandomAccessCode.generateCode();
	}

	void OnServerInitialized () {
		print ("Servidor Iniciado");
	}

	void OnMasterServerEvent(MasterServerEvent mse){
		if(mse == MasterServerEvent.RegistrationSucceeded){
			master = true;
		}
	}

	void OnGUI(){

		GUI.skin = skin;
		GUILayout.BeginArea(new Rect(0,0,Screen.width,Screen.height));
		GUILayout.Label("Reh-app-ilitacion");

		if(master)
			GUILayout.Label("Clave de acceso: "+ randomCode);
		else{
			if(GUILayout.Button("Iniciar Servidor")){
				Network.InitializeServer(1,port,!Network.HavePublicAddress());
				MasterServer.RegisterHost("SODVI","SODVI_" + randomCode);
			}
		}

		if(Network.connections.Length > 0){
			GUILayout.Label ("Dispositivo sincronizado");
			if(GUILayout.Button("Iniciar juego"))
				Application.LoadLevel("Level Test");
		}
		
		GUILayout.EndArea();	
	}
#endif
}
