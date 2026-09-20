using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Setup")]
    [Tooltip("Tarik objek Player dari Hierarchy ke ruang ini")]
    public Transform target; 

    [Header("Camera Settings")]
    public Vector3 offset = new Vector3(0f, 6f, -8f); // Kedudukan kamera (Tinggi sikit, ke belakang sikit)
    public float smoothSpeed = 5f; // Kadar kelicinan kamera mengekori pemain

    void LateUpdate()
    {
        // Kalau tak ada target, jangan buat apa-apa (elak error)
        if (target == null) return;

        // 1. Kira posisi di mana kamera sepatutnya berada (Posisi Player + Jarak Offset)
        Vector3 desiredPosition = target.position + offset;
        
        // 2. Gerakkan kamera dari kedudukan sekarang ke kedudukan baharu secara perlahan (Lerp)
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // 3. Pastikan lensa kamera sentiasa menunduk sikit memandang ke arah tengah badan/kepala Player
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
