using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FriendSystem : MonoBehaviour
{
    private string username = "Saul Goodman";

    private void Start()
    {
        GetUserData(username);
    }

    public void Register(string username)
    {
        this.username = username;
        StartCoroutine(RegisterUserCoroutine(username));
    }

    IEnumerator RegisterUserCoroutine(string username)
    {
        string url = "https://scatological-matter.000webhostapp.com/register.php";
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        using (WWW www = new WWW(url, form))
        {
            yield return www;
            if (www.error != null)
            {
                Debug.LogError("RegisterUser error: " + www.error);
            }
            else
            {
                Debug.Log("RegisterUser response: " + www.text);
                // Handle the response from the API here
            }
        }
    }

    public void GetUserData(string username)
    {
        StartCoroutine(GetUserDataCoroutine(username));
    }

    IEnumerator GetUserDataCoroutine(string username)
    {
        string url = "https://scatological-matter.000webhostapp.com/get_user_data.php";
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        using (WWW www = new WWW(url, form))
        {
            yield return www;
            if (www.error != null)
            {
                Debug.LogError("GetUserData error: " + www.error);
            }
            else
            {
                Debug.Log("GetUserData response: " + www.text);
                // Parse the JSON response into a UserData object
                UserData userData = JsonUtility.FromJson<UserData>(www.text);
                // Access the individual properties of the UserData object
                //Debug.Log("Incoming requests: " + userData.incoming_requests);
                //Debug.Log("Unfriends: " + userData.unfriends);
                //Debug.Log("New friends: " + userData.new_friends);

                string[] incoming_requests = userData.incoming_requests.Split(',');
                string[] unfriends = userData.unfriends.Split(',');
                string[] new_friends = userData.new_friends.Split(',');

                for (int i = 0; i < incoming_requests.Length; i++)
                {
                    Debug.Log("Incoming requests: " + incoming_requests[i]);
                }

                for (int i = 0; i < unfriends.Length; i++)
                {
                    Debug.Log("Unfriends: " + unfriends[i]);
                }

                for (int i = 0; i < new_friends.Length; i++)
                {
                    Debug.Log("New friends: " + new_friends[i]);
                }
            }
        }
    }

    // Define the UserData class to match the structure of the API response
    [System.Serializable]
    public class UserData
    {
        public string incoming_requests;
        public string unfriends;
        public string new_friends;
    }

    public void SendRequest(string friendUsername)
    {
        StartCoroutine(SendFriendRequestCoroutine(friendUsername));
    }

    IEnumerator SendFriendRequestCoroutine(string friendUsername)
    {
        string url = "https://scatological-matter.000webhostapp.com/send_friend_request.php";
        WWWForm form = new WWWForm();
        form.AddField("sender_username", username);
        form.AddField("receiver_username", friendUsername);
        using (WWW www = new WWW(url, form))
        {
            yield return www;
            if (www.error != null)
            {
                Debug.LogError("SendFriendRequest error: " + www.error);
            }
            else
            {
                Debug.Log("SendFriendRequest response: " + www.text);
                // Handle the response from the API here
            }
        }
    }

    public void AcceptRequest(string friendUsername)
    {
        StartCoroutine(AcceptFriendRequestCoroutine(friendUsername));
    }

    IEnumerator AcceptFriendRequestCoroutine(string friendUsername)
    {
        string url = "https://scatological-matter.000webhostapp.com/accept_friend_request.php";
        WWWForm form = new WWWForm();
        form.AddField("accepter_username", friendUsername);
        form.AddField("requester_username", username);
        using (WWW www = new WWW(url, form))
        {
            yield return www;
            if (www.error != null)
            {
                Debug.LogError("AcceptFriendRequest error: " + www.error);
            }
            else
            {
                Debug.Log("AcceptFriendRequest response: " + www.text);
                // Handle the response from the API here
            }
        }
    }
    
    public void DenyRequest(string friendUsername)
    {
        StartCoroutine(DenyFriendRequestCoroutine(friendUsername));
    }

    IEnumerator DenyFriendRequestCoroutine(string friendUsername)
    {
        string url = "https://scatological-matter.000webhostapp.com/deny_friend_request.php";
        WWWForm form = new WWWForm();
        form.AddField("current_user", username);
        form.AddField("friend_username", friendUsername);
        using (WWW www = new WWW(url, form))
        {
            yield return www;
            if (www.error != null)
            {
                Debug.LogError("Deny friend request error: " + www.error);
            }
            else
            {
                Debug.Log("Deny friend request response: " + www.text);
                // Handle the response from the API here
            }
        }
    }

    public void Unfriend(string friendUsername)
    {
        StartCoroutine(UnfriendCoroutine(friendUsername));
    }

    IEnumerator UnfriendCoroutine(string friendUsername)
    {
        string url = "https://scatological-matter.000webhostapp.com/remove_friend.php";
        WWWForm form = new WWWForm();
        form.AddField("remover_username", username);
        form.AddField("removee_username", friendUsername);
        using (WWW www = new WWW(url, form))
        {
            yield return www;
            if (www.error != null)
            {
                Debug.LogError("Unfriend error: " + www.error);
            }
            else
            {
                Debug.Log("Unfriend response: " + www.text);
                // Handle the response from the API here
            }
        }
    }
}