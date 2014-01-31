using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class MainMenu : MonoBehaviour {

	public GUISkin skin;
	public int port = 25000;

#if UNITY_ANDROID
	
	private bool refreshingHosts;
	string name = "";

	int option = 0;
	string[] a = {"Izquierdo","Derecho"};

	void Start(){
		if(PlayerPrefs.HasKey("Name"))
			name = PlayerPrefs.GetString("Name");
		
	}

	void Update(){

		if(refreshingHosts){
			HostData[] hostData;

			if(MasterServer.PollHostList().Length >0){
				refreshingHosts = false;
				hostData = MasterServer.PollHostList();

				if(hostData.Length > 0){
					string temp = "SODVI_"+name;
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
		if(PlayerPrefs.HasKey("Name")){
			GUILayout.Label("Bienvenido "+ PlayerPrefs.GetString("Name"));

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
			
			if(GUILayout.Button("Borra Usuario")){
				PlayerPrefs.DeleteKey("Name");
				Network.Disconnect();
			}
		}else{
			GUILayout.Label("Nombre de usuario: ");
			name = GUILayout.TextField(name);
			if(GUILayout.Button("Ingresar datos"))
				PlayerPrefs.SetString("Name",name);
		}
		GUILayout.EndArea();
	}
#else
	string name = "";

	void Start(){
		if(PlayerPrefs.HasKey("Name"))
			name = PlayerPrefs.GetString("Name");
		
	}

	void OnServerInitialized () {
		print ("Servidor Iniciado");
	}

	void OnMasterServerEvent(MasterServerEvent mse){
		if(mse == MasterServerEvent.RegistrationSucceeded){
			print ("Servidor registrado");
		}
	}

	void OnGUI(){
		GUI.skin = skin;
		GUILayout.BeginArea(new Rect(0,0,Screen.width,Screen.height));
		GUILayout.Label("Reh-app-ilitacion");
		
		if(PlayerPrefs.HasKey("Name")){

			GUILayout.Label("Bienvenido "+ PlayerPrefs.GetString("Name"));
			GUILayout.Label("N: "+Network.connections.Length);
			if(Network.connections.Length == 0){
				if(GUILayout.Button("Iniciar Servidor")){
					Network.InitializeServer(1,port,!Network.HavePublicAddress());
					MasterServer.RegisterHost("SODVI","SODVI_"+PlayerPrefs.GetString("Name"));
				}
			}else{
				GUILayout.Label ("Dispositivo sincronizado");
				if(GUILayout.Button("Iniciar sesion"))
					Application.LoadLevel("Level Test");
			}

			if(GUILayout.Button("Borra Usuario")){
				PlayerPrefs.DeleteKey("Name");
				Network.Disconnect();
			}
		}else{
			GUILayout.Label("Nombre de usuario: ");
			name = GUILayout.TextField(name);
			if(GUILayout.Button("Crear perfil"))
				PlayerPrefs.SetString("Name",name);
		}
		GUILayout.EndArea();	
	}
#endif


}
