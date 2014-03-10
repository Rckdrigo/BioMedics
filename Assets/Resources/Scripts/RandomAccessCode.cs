using UnityEngine;
using System.Collections;

public class RandomAccessCode : MonoBehaviour {

	private const string _chars = "abcdefghijklmnopqrstuvwxyz";
	bool exists;

	public static string generateCode(){
		MasterServer.RequestHostList("SODVI");
		HostData[] hostData = MasterServer.PollHostList();
		bool exists;
		string random;

		do{
			exists = false;
			random = randomChars(6);
			foreach(HostData host in hostData)
				if(host.gameName == random)
					exists = true;
		}while (exists)	;
		

		return random;
	}
	private static string randomChars(int size){
		string random = "";

		for(int i = 0; i < size ; i++)
			random+= _chars[Random.Range(0,_chars.Length-1)];

		return random;
	}
}
