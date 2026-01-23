using UnityEngine;

public class WaterReset : MonoBehaviour
{
    [Header("Referensi")]
    public Transform respawnPoint;      
    public HouseManager houseManager; 

    void OnTriggerEnter(Collider other)
    {
        // Mendeteksi objek dengan Tag Player (seperti yang sudah Anda pasang di VR Player)
        if (other.CompareTag("Player") || other.transform.root.CompareTag("Player"))
        {
            // Ambil objek induk teratas (VR Player)
            Transform rootPlayer = other.transform.root;
            
            CharacterController cc = rootPlayer.GetComponentInChildren<CharacterController>();
            
            // 1. Matikan Character Controller (WAJIB agar tidak terpental/stuck)
            if (cc != null) cc.enabled = false;

            // 2. Pindahkan posisi menggunakan World Space agar tidak 'nyasar'
            rootPlayer.SetPositionAndRotation(respawnPoint.position, respawnPoint.rotation);

            // 3. Reset Kecepatan (jika ada Rigidbody pada tangan atau badan)
            Rigidbody[] rbs = rootPlayer.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in rbs) {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            // 4. Nyalakan kembali CC
            if (cc != null) cc.enabled = true;

            // 5. Reset progres di HouseManager
            if (houseManager != null) houseManager.ResetProgress();

            Debug.Log("Teleport Berhasil ke: " + respawnPoint.position);
        }
    }
}