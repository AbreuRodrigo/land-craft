using UnityEngine;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

public class ProgressManager {
	private const string FILE = "/bin.dat";

	private Stage tempRetrievedStage;

	private ProgressData data;
	public ProgressData Data{
		get { 
			LoadData();			
			return data; 
		}
	}

	private HashManager hashManager;
	public HashManager HashManager {
		get { return hashManager; }
	}

	private XD xd;

	public ProgressManager(HashManager hm) {
		hashManager = hm;

		LoadData();
	}

	public void SaveData() {
		BinaryFormatter bf = new BinaryFormatter();
		
		FileStream file = CreateDataFile();

		if(data != null && xd != null) {
			xd.px = data.playerXP;
			xd.pl = data.playerLevel;
			xd.cs = data.currentStage;
		}

		bf.Serialize(file, xd);
		
		file.Close();
	}
	
	private void LoadData() {
		this.CreateDataFileIfNotExists();
		
		if(File.Exists(PersistencePath)) {
			BinaryFormatter bf = new BinaryFormatter();
			
			FileStream file = OpenDataFile();
			
			xd = (XD)bf.Deserialize(file);
			XdToProgressData(xd);
			
			file.Close();
		}
	}
	
	public void ResetData() {
		xd = new XD();
		data = new ProgressData();
		SaveData();
	}

	private FileStream CreateDataFile() {
		return File.Create(PersistencePath);
	}
	
	private FileStream OpenDataFile() {
		return File.Open(PersistencePath, FileMode.Open);
	}
	
	private void InstantiateDataIfNull() {
		if(xd == null) {
			xd = new XD();
		}
		if(data == null) {
			data = new ProgressData();
		}
	}
	
	private void CreateDataFileIfNotExists() {
		if(!File.Exists(PersistencePath)) {
			xd = new XD();
			xd.cs = 1;
			xd.pl = 1;
			xd.px = 0;
			xd.ss = new XS[hashManager.hashedNumbers.Keys.Count];

			int i = 0;

			foreach(string key in hashManager.hashedNumbers.Keys) {
				XS xs = new XS(i, key);
				if(i == 0) {
					xs.e = false;
					xs.f = true;
				}

				AddStageToXD(i, xs);

				i++;
			}

			SaveData();
		}
	}
	
	private string PersistencePath {
		get { return Application.persistentDataPath + FILE; }
	}

	private void XdToProgressData(XD xd) {
		if(xd != null) {
			data = new ProgressData();
			data.currentStage = xd.cs;
			data.playerLevel = xd.pl;
			data.playerXP = xd.px;

			int i = 0;

			if(xd.ss != null) {
				data.stages = new Stage[xd.ss.Length];

				foreach(XS xs in xd.ss) {
					AddStageToData(i, new Stage(xs));
					i++;
				}
			}
		}
	}

	public bool IsStageLocked(int index) {
		if(data != null  && data.stages != null && index > 0 && index < data.stages.Length) {
			tempRetrievedStage = data.stages[index];

			if(tempRetrievedStage.isLocked && !tempRetrievedStage.isClear) {
				return true;
			}
		}

		return false;
	}

	public void AddStageToData(int index, Stage stage) {
		if(data.stages != null  && (index >= 0 && index < data.stages.Length)) {
			data.stages[index] = stage;
			xd.ss[index] = new XS(stage);
		}
	}

	public void AddStageToXD(int i, XS s) {
		if(xd.ss != null  && i < xd.ss.Length) {
			xd.ss[i] = s;
		}
	}

	public void AdvanceToNextStage() {
		if(PlayerPrefsManager.ClickedStage == data.currentStage + 1) {
			data.currentStage++;
			SaveData();
		}
	}
}