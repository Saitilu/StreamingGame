using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputAccount : MonoBehaviour
{
    public InputField inputField;
    [SerializeField] GameObject prefab;

    public void StartGame()
    {
        //if Account already stored, destroy to replace
        GameObject prevStore = GameObject.Find("Account");
        if (prevStore != null)
            Destroy(prevStore);

        //create new Account store
        GameObject store = Instantiate(prefab, new Vector3(0,0,0), Quaternion.identity);
        store.name = "Account";
        Account account = store.GetComponent<Account>();
        account.AccountName = inputField.text;
        DontDestroyOnLoad(store);

        //switch scenes
        SceneManager.LoadScene("Game");
    }


}
