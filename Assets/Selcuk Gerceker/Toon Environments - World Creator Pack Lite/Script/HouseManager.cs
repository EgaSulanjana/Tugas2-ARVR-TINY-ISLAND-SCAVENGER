using UnityEngine;
using TMPro;

public class HouseManager : MonoBehaviour
{
    [Header("Target Koleksi")]
    public int bungaTarget = 3;
    public int batuTarget = 3;
    public int kayuTarget = 3;

    [Header("Skor Saat Ini")]
    private int bungaSkor = 0;
    private int batuSkor = 0;
    private int kayuSkor = 0;

    [Header("UI Informasi")]
    public TextMeshProUGUI infoText;

    private bool sedangMenampilkanPeringatan = false;

    void Start()
    {
        // Memastikan UI menampilkan status awal 0/3 saat game dimulai
        UpdateUI(); 
    }

    void OnTriggerEnter(Collider other)
    {
        // 1. Logika untuk Bunga (Pastikan Tag di Inspector: Bunga)
        if (other.CompareTag("bunga"))
        {
            if (bungaSkor < bungaTarget)
            {
                bungaSkor++;
                Destroy(other.gameObject); // Objek dihancurkan agar tidak menumpuk
                UpdateUI();
            }
            else TampilkanPeringatan("Bunga sudah penuh!");
        }
        // 2. Logika untuk Batu (Pastikan Tag di Inspector: Batu)
        else if (other.CompareTag("batuBesar"))
        {
            if (batuSkor < batuTarget)
            {
                batuSkor++;
                Destroy(other.gameObject);
                UpdateUI();
            }
            else TampilkanPeringatan("Batu sudah penuh!");
        }
        // 3. Logika untuk Kayu (Pastikan Tag di Inspector: Kayu)
        else if (other.CompareTag("kayu"))
        {
            if (kayuSkor < kayuTarget)
            {
                kayuSkor++;
                Destroy(other.gameObject);
                UpdateUI();
            }
            else TampilkanPeringatan("Kayu sudah penuh!");
        }
        // 4. Peringatan jika objek salah atau tidak memiliki Tag di atas
        else
        {
            TampilkanPeringatan("Peringatan: Objek ini salah!");
            // Opsional: Hancurkan objek salah agar area pintu tidak macet/penuh
            Destroy(other.gameObject); 
        }

        CekPemenang();
    }

    public void UpdateUI()
    {
        sedangMenampilkanPeringatan = false;
        infoText.text = $"Sisa Tugas:\nBunga: {bungaSkor}/{bungaTarget}\nBatu: {batuSkor}/{batuTarget}\nKayu: {kayuSkor}/{kayuTarget}";
    }

    void TampilkanPeringatan(string pesan)
    {
        sedangMenampilkanPeringatan = true;
        infoText.text = pesan;
        
        // Membatalkan Invoke sebelumnya jika ada, agar durasi tidak tumpang tindih
        CancelInvoke("UpdateUI"); 
        
        // Mengembalikan tampilan ke status skor setelah 2 detik
        Invoke("UpdateUI", 2.0f); 
    }

    void CekPemenang()
    {
        if (bungaSkor >= bungaTarget && batuSkor >= batuTarget && kayuSkor >= kayuTarget)
        {
            CancelInvoke("UpdateUI");
            infoText.text = "<color=yellow>SELAMAT!\nANDA MENANG! :D</color>";
        }
    }

    // Fungsi ini dipanggil oleh script WaterReset saat player jatuh ke air
    public void ResetProgress()
    {
        bungaSkor = 0;
        batuSkor = 0;
        kayuSkor = 0;
        UpdateUI();
    }
}