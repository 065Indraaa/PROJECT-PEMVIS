# ✅ CHECKLIST - FormDataGuru Terhubung ke Database

## 🔗 KONEKSI SUDAH DIBUAT

### Di FormDataGuru.vb (Line 9-17):
```vb
Sub koneksi()
    conn = New MySqlConnection("server=localhost;user=root;password=;database=db_sekolah")
    Try
        conn.Open()
    Catch ex As Exception
        MsgBox("Koneksi Gagal: " & ex.Message)
    End Try
End Sub
```

---

## 📊 MAPPING LENGKAP: TextBox → Database

| Input Form | Control | Database Table | Column Database |
|------------|---------|----------------|-----------------|
| Kode Guru | **TextBox1** | `tb_guru` | `nip` |
| Nama Lengkap | **TextBox2** | `tb_guru` | `nama_lengkap` |
| Mata Pelajaran | **TextBox4** | `tb_guru` | `mata_pelajaran` |
| Jenis Kelamin | **RadioButton1/2** | `tb_guru` | `jenis_kelamin` |
| Alamat | **TextBox3** | `tb_guru` | `alamat` |
| No Telepon | **TextBox5** | `tb_guru` | `no_telepon` |
| Search | **TextBox8** | - | (untuk cari) |

---

## 📋 MAPPING: DataGridView → Database

| DataGridView Column | Index | Database Column |
|---------------------|-------|-----------------|
| **KodeGuru** | 0 | `nip` |
| **NamaGuru** | 1 | `nama_lengkap` |
| **Mapel** | 2 | `mata_pelajaran` |
| **JenisKelamin** | 3 | `jenis_kelamin` |
| **Alamat** | 4 | `alamat` |
| **NoTlpn** | 5 | `no_telepon` |

---

## 🔄 FUNGSI SUDAH TERHUBUNG

### ✅ 1. Form Load (Line 50-52)
```vb
Private Sub FormDataGuru_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    TampilData()
End Sub
```
**Fungsi:** Otomatis load data dari database saat form dibuka

---

### ✅ 2. TampilData (Line 20-35)
```vb
Sub TampilData()
    DataGridView1.Rows.Clear()
    koneksi()
    cmd = New MySqlCommand("SELECT nip, nama_lengkap, mata_pelajaran, jenis_kelamin, alamat, no_telepon FROM tb_guru ORDER BY id_guru", conn)
    dr = cmd.ExecuteReader
    While dr.Read
        DataGridView1.Rows.Add(dr(0), dr(1), dr(2), dr(3), dr(4), dr(5))
    End While
    dr.Close()
    conn.Close()
End Sub
```
**Fungsi:** Menampilkan semua data guru dari database ke DataGridView

---

### ✅ 3. Button Simpan (Line 55-85)
```vb
Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    koneksi()
    cmd = New MySqlCommand("INSERT INTO tb_guru (nip, nama_lengkap, mata_pelajaran, jenis_kelamin, alamat, no_telepon) VALUES (@nip, @nama, @mapel, @jk, @alamat, @telp)", conn)
    cmd.Parameters.AddWithValue("@nip", TextBox1.Text)
    cmd.Parameters.AddWithValue("@nama", TextBox2.Text)
    cmd.Parameters.AddWithValue("@mapel", TextBox4.Text)
    cmd.Parameters.AddWithValue("@jk", jk)
    cmd.Parameters.AddWithValue("@alamat", TextBox3.Text)
    cmd.Parameters.AddWithValue("@telp", TextBox5.Text)
    i = cmd.ExecuteNonQuery()
End Sub
```
**Fungsi:** Menyimpan data dari TextBox ke database `tb_guru`

---

### ✅ 4. Button Edit (Line 88-118)
```vb
Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
    koneksi()
    cmd = New MySqlCommand("UPDATE tb_guru SET nama_lengkap=@nama, mata_pelajaran=@mapel, jenis_kelamin=@jk, alamat=@alamat, no_telepon=@telp WHERE nip=@nip", conn)
    cmd.Parameters.AddWithValue("@nip", TextBox1.Text)
    ' ... parameters lainnya
    i = cmd.ExecuteNonQuery()
End Sub
```
**Fungsi:** Update data di database berdasarkan NIP

---

### ✅ 5. Button Hapus (Line 121-144)
```vb
Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
    koneksi()
    cmd = New MySqlCommand("DELETE FROM tb_guru WHERE nip=@nip", conn)
    cmd.Parameters.AddWithValue("@nip", TextBox1.Text)
    i = cmd.ExecuteNonQuery()
End Sub
```
**Fungsi:** Menghapus data dari database berdasarkan NIP

---

### ✅ 6. Button Cari (Line 152-168)
```vb
Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
    koneksi()
    cmd = New MySqlCommand("SELECT nip, nama_lengkap, mata_pelajaran, jenis_kelamin, alamat, no_telepon FROM tb_guru WHERE nip LIKE @cari OR nama_lengkap LIKE @cari", conn)
    cmd.Parameters.AddWithValue("@cari", "%" & TextBox8.Text & "%")
    dr = cmd.ExecuteReader
End Sub
```
**Fungsi:** Mencari data di database berdasarkan NIP atau Nama

---

### ✅ 7. DataGridView Click (Line 171-185)
```vb
Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
    Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
    TextBox1.Text = row.Cells(0).Value.ToString()  ' NIP
    TextBox2.Text = row.Cells(1).Value.ToString()  ' Nama
    TextBox4.Text = row.Cells(2).Value.ToString()  ' Mapel
    If row.Cells(3).Value.ToString() = "Laki-Laki" Then
        RadioButton1.Checked = True
    Else
        RadioButton2.Checked = True
    End If
    TextBox3.Text = row.Cells(4).Value.ToString()  ' Alamat
    TextBox5.Text = row.Cells(5).Value.ToString()  ' No Telp
End Sub
```
**Fungsi:** Klik baris di DataGridView → isi otomatis ke TextBox

---

## 🧪 CARA TEST

### 1. Test Koneksi Database
Buka form, jika muncul data di DataGridView = **BERHASIL TERHUBUNG** ✅

### 2. Test Button Simpan
1. Isi TextBox1: `NIP003`
2. Isi TextBox2: `Test Guru`
3. Isi TextBox4: `Matematika`
4. Pilih RadioButton jenis kelamin
5. Isi TextBox3: `Jl. Test`
6. Isi TextBox5: `08123456789`
7. Klik Button1 (Simpan)
8. Cek DataGridView → data baru muncul ✅

### 3. Test Button Edit
1. Klik baris di DataGridView
2. Ubah TextBox2 (Nama)
3. Klik Button3 (Edit)
4. Cek DataGridView → data terupdate ✅

### 4. Test Button Hapus
1. Klik baris di DataGridView
2. Klik Button4 (Hapus)
3. Klik Yes
4. Cek DataGridView → data terhapus ✅

### 5. Test Button Cari
1. Ketik di TextBox8: `NIP001`
2. Klik Button6 (Cari)
3. DataGridView filter → hanya tampil yang sesuai ✅

---

## ⚠️ TROUBLESHOOTING

**Error: "Koneksi Gagal"**
- ✅ Pastikan MySQL running
- ✅ Pastikan sudah import `db_sekolah_4form.sql`
- ✅ Cek username: `root`, password: kosong
- ✅ Cek port: 3306 (default) atau 3307

**Error: "Table doesn't exist"**
```sql
USE db_sekolah;
SHOW TABLES;
```
Harus ada: `tb_guru`

**DataGridView kosong saat load:**
```sql
SELECT * FROM tb_guru;
```
Pastikan ada sample data

---

## ✅ KESIMPULAN

### SUDAH TERHUBUNG 100%:
- ✅ TextBox1 → `tb_guru.nip`
- ✅ TextBox2 → `tb_guru.nama_lengkap`
- ✅ TextBox4 → `tb_guru.mata_pelajaran`
- ✅ RadioButton1/2 → `tb_guru.jenis_kelamin`
- ✅ TextBox3 → `tb_guru.alamat`
- ✅ TextBox5 → `tb_guru.no_telepon`
- ✅ DataGridView → SELECT dari `tb_guru`
- ✅ Button Simpan → INSERT ke `tb_guru`
- ✅ Button Edit → UPDATE `tb_guru`
- ✅ Button Hapus → DELETE dari `tb_guru`
- ✅ Button Cari → SELECT dengan WHERE LIKE

**FormDataGuru SIAP DIGUNAKAN!** 🎉
