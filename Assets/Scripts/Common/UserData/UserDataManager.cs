using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : SingletonBehaviour<UserDataManager>
{
    // 저장된 유저 데이터 존재 여부
    public bool ExistsSaveData {  get; private set; }
    // 모든 유저 데이터 인스턴스를 저장하는 컨데이너
    public List<IUserData> UserDataList { get; private set; } = new List<IUserData>();

    protected override void Init()
    {
        base.Init();

        // 모든 유저 데이터를 UserDataList에 추가
        UserDataList.Add(new UserSettingsData());
        UserDataList.Add(new UserGoodsData());
    }

    public void SetDefaultUserData()
    {
        for(int i = 0; i < UserDataList.Count; i++)
        {
            UserDataList[i].SetDefaultData();
        }
    }

    public void LoadUserData()
    {
        ExistsSaveData = PlayerPrefs.GetInt("ExistsSaveData") == 1 ? true : false;

        for (int i = 0; i < UserDataList.Count; i++)
        {
            UserDataList[i].LoadData();
        }
    }

    public void SaveUserData()
    {
        bool hasSaveError = false;

        for (int i = 0; i < UserDataList.Count; i++)
        {
            bool isSaveSuccess = UserDataList[i].SaveData();
            if(!isSaveSuccess)
            {
                hasSaveError = true;
            }
        }

        if (!hasSaveError)
        {
            ExistsSaveData = true;
            PlayerPrefs.SetInt("ExistsSaveData", 1);
            PlayerPrefs.Save();

        }
    }
}
