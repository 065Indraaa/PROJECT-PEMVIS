# 👨‍🎓 PANDUAN FORM DATA SISWA

## 🎯 FITUR LENGKAP

### ✅ CRUD Operations + Auto Create User:
- **Create:** Simpan siswa baru + auto create user login
- **Read:** Tampil data di DataGridView
- **Update:** Edit data siswa
- **Delete:** Hapus data siswa (+ user otomatis terhapus)
- **Search:** Cari berdasarkan NIS/Nama
- **ComboBox Kelas:** Diambil dari tb_kelas (DataKelas)

---

## 📊 MAPPING: Form ↔ Database

### Input Controls:
| Form Control | Type | Database Column | Keterangan |
|--------------|------|-----------------|------------|
| `TextBox1` | TextBox | `nis` | NIS siswa (username login) |
| `TextBox2` | TextBox | `nama_lengkap` | Nama lengkap siswa |
| `RadioButton1` | Radio | `jenis_kelamin` | Laki-Laki |
| `RadioButton2` | Radio | `jenis_kelamin` | Perempuan |
| `TextBox3` | TextBox | `tanggal_lahir` | TTL (format: YYYY-MM-DD) |
| `ComboBox1` | ComboBox | `id_kelas` | **Kelas dari tb_kelas** |
| `TextBox5` | TextBox | `alamat` | Alamat siswa |
| `TextBox6` | TextBox | `nama_ayah` | Nama ayah |
| `TextBox7` | TextBox | `nama_ibu` | Nama ibu |
| `TextBox8` | TextBox | - | Search box |

### DataGridView Columns:
| Column | Index | Database | Keterangan |
|--------|-------|----------|------------|
| NIS | 0 | `nis` | Nomor Induk Siswa |
| Nama | 1 | `nama_lengkap` | Nama lengkap |
| JenisKelamin | 2 | `jenis_kelamin` | Laki-Laki/Perempuan |
| TTL | 3 | `tanggal_lahir` | Tanggal lahir |
| Kelas | 4 | `nama_kelas` | Dari JOIN tb_kelas |
| Alamat | 5 | `alamat` | Alamat |
| Ayah | 6 | `nama_ayah` | Nama ayah |
| Ibu | 7 | `nama_ibu` | Nama ibu |

### Buttons:
| Button | Fungsi | Keterangan |
|--------|--------|------------|
| Button1 | Simpan | Simpan siswa baru + auto create user |
| Button2 | Edit | Update data siswa |
| Button3 | Cancel | Bersihkan form |
| Button4 | Hapus | Hapus data siswa + user |
| Button5 | Close | Tutup form |
| Button6 | Cari | Search NIS/Nama |

---

## 🔑 COMBOBOX KELAS (DARI tb_kelas)

### Load Kelas di Form Load:
```vb
Sub LoadKelas()
    ComboBox1.Items.Clear()
    koneksi()
    cmd = New MySqlCommand("SELECT nama_kelas FROM tb_kelas ORDER BY nama_kelas", conn)
    dr = cmd.ExecuteReader
    While dr.Read
        ComboBox1.Items.Add(dr("nama_kelas").ToString())
    End While
    dr.Close()
    conn.Close()
End Sub
```

**ComboBox1 akan berisi:**
- X-1
- X-2
- XI-1
- XI-2
- XII-1
- XII-2

### Get ID Kelas:
```vb
Function GetIdKelas(namaKelas As String) As Integer
    koneksi()
    cmd = New MySqlCommand("SELECT id_kelas FROM tb_kelas WHERE nama_kelas=@nama", conn)
    cmd.Parameters.AddWithValue("@nama", namaKelas)
    dr = cmd.ExecuteReader
    If dr.Read Then
        idKelas = Convert.ToInt32(dr("id_kelas"))
    End If
    Return idKelas
End Function
```

---

## 🚀 AUTO CREATE USER

### Saat Simpan Siswa Baru:
```vb
Private Sub Button1_Click(...) Handles Button1.Click
    ' Validasi
    If TextBox1.Text = "" Or TextBox2.Text = "" Then
        MsgBox("NIS dan Nama harus diisi!")
        Return
    End If
    
    ' Get ID Kelas dari ComboBox
    Dim idKelas As Integer = GetIdKelas(ComboBox1.Text)
    
    ' Gunakan Stored Procedure
    cmd = New MySqlCommand("sp_insert_siswa", conn)
    cmd.CommandType = CommandType.StoredProcedure
    cmd.Parameters.AddWithValue("@p_nis", TextBox1.Text)
    cmd.Parameters.AddWithValue("@p_nama_lengkap", TextBox2.Text)
    cmd.Parameters.AddWithValue("@p_jenis_kelamin", jk)
    cmd.Parameters.AddWithValue("@p_tanggal_lahir", TextBox3.Text)
    cmd.Parameters.AddWithValue("@p_id_kelas", idKelas)
    cmd.Parameters.AddWithValue("@p_alamat", TextBox5.Text)
    cmd.Parameters.AddWithValue("@p_nama_ayah", TextBox6.Text)
    cmd.Parameters.AddWithValue("@p_nama_ibu", TextBox7.Text)
    cmd.ExecuteNonQuery()
    
    ' Ambil nama depan untuk password
    Dim namaDepan As String = TextBox2.Text.Split(" "c)(0)
    
    MsgBox("Data Berhasil Disimpan!" & vbCrLf & _
           "Username: " & TextBox1.Text & vbCrLf & _
           "Password: " & namaDepan)
End Sub
```

---

## 🔄 ALUR KERJA

### 1. Tambah Siswa Baru:
```
User mengisi form:
  TextBox1 (NIS):     "NIS004"
  TextBox2 (Nama):    "Andi Pratama"
  RadioButton1:       Laki-Laki ✓
  TextBox3 (TTL):     "2008-05-15"
  ComboBox1 (Kelas):  "X-2" (pilih dari dropdown)
  TextBox5 (Alamat):  "Jl. Sudirman No. 10"
  TextBox6 (Ayah):    "Bapak Andi"
  TextBox7 (Ibu):     "Ibu Andi"

↓ Klik Button1 (Simpan)

System otomatis:
  1. Get id_kelas dari ComboBox1.Text ("X-2" → id_kelas = 2)
  2. CALL sp_insert_siswa(...)
  3. Di stored procedure:
     - INSERT ke tb_users:
       username = "NIS004"
       password = "Andi" (nama depan)
       id_role = 3 (Siswa)
       status = "Aktif"
     - INSERT ke tb_siswa dengan id_user

Muncul pesan:
  ✅ "Data Berhasil Disimpan!
      Username: NIS004
      Password: Andi"

Siswa bisa langsung login!
```

### 2. Edit Siswa:
```
User klik baris di DataGridView
  ↓ Form terisi otomatis
  ↓ ComboBox1 juga terisi dengan nama kelas

User ubah data (contoh: ganti kelas)
  ComboBox1: "X-2" → "XI-1"

↓ Klik Button2 (Edit)

System:
  1. Get id_kelas dari "XI-1"
  2. UPDATE tb_siswa SET ... WHERE nis=...
  3. Refresh DataGridView

✅ Data terupdate
```

### 3. Hapus Siswa:
```
User klik baris di DataGridView
↓ Klik Button4 (Hapus)
↓ MsgBox "Yakin hapus data siswa ini?"
↓ User klik Yes

System:
  1. DELETE FROM tb_siswa WHERE nis=...
  2. User di tb_users otomatis terhapus (ON DELETE CASCADE)

✅ Data terhapus
```

---

## 💻 IMPLEMENTASI VB.NET

### 1. Form Load
```vb
Private Sub FormDataSiswa_Load(...) Handles MyBase.Load
    LoadKelas()   ' Load kelas ke ComboBox
    TampilData()  ' Load data ke DataGridView
End Sub
```

### 2. Tampil Data dengan JOIN
```vb
Sub TampilData()
    cmd = New MySqlCommand("SELECT s.nis, s.nama_lengkap, s.jenis_kelamin, s.tanggal_lahir, k.nama_kelas, s.alamat, s.nama_ayah, s.nama_ibu FROM tb_siswa s LEFT JOIN tb_kelas k ON s.id_kelas = k.id_kelas ORDER BY s.id_siswa", conn)
    dr = cmd.ExecuteReader
    While dr.Read
        DataGridView1.Rows.Add(dr(0), dr(1), dr(2), dr(3), dr(4), dr(5), dr(6), dr(7))
    End While
End Sub
```

### 3. DataGridView Click
```vb
Private Sub DataGridView1_CellClick(...) Handles DataGridView1.CellClick
    If e.RowIndex >= 0 Then
        Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        TextBox1.Text = row.Cells(0).Value.ToString() ' NIS
        TextBox2.Text = row.Cells(1).Value.ToString() ' Nama
        
        ' Jenis Kelamin
        If row.Cells(2).Value.ToString() = "Laki-Laki" Then
            RadioButton1.Checked = True
        Else
            RadioButton2.Checked = True
        End If
        
        TextBox3.Text = row.Cells(3).Value.ToString() ' TTL
        ComboBox1.Text = row.Cells(4).Value.ToString() ' Kelas
        TextBox5.Text = row.Cells(5).Value.ToString() ' Alamat
        TextBox6.Text = row.Cells(6).Value.ToString() ' Ayah
        TextBox7.Text = row.Cells(7).Value.ToString() ' Ibu
    End If
End Sub
```

---

## 📋 DATABASE QUERY

### SELECT dengan JOIN:
```sql
SELECT s.nis, s.nama_lengkap, s.jenis_kelamin, s.tanggal_lahir, 
       k.nama_kelas, s.alamat, s.nama_ayah, s.nama_ibu
FROM tb_siswa s
LEFT JOIN tb_kelas k ON s.id_kelas = k.id_kelas
ORDER BY s.id_siswa
```

### INSERT dengan Stored Procedure:
```sql
CALL sp_insert_siswa(
    'NIS004',           -- NIS
    'Andi Pratama',     -- Nama Lengkap
    'Laki-Laki',        -- Jenis Kelamin
    '2008-05-15',       -- Tanggal Lahir
    2,                  -- ID Kelas (X-2)
    'Jl. Sudirman',     -- Alamat
    'Bapak Andi',       -- Nama Ayah
    'Ibu Andi'          -- Nama Ibu
)
```

**Hasil:**
- ✅ User dibuat di `tb_users` (username=NIS004, password=Andi)
- ✅ Siswa dibuat di `tb_siswa`

### UPDATE:
```sql
UPDATE tb_siswa 
SET nama_lengkap=..., jenis_kelamin=..., tanggal_lahir=..., 
    id_kelas=..., alamat=..., nama_ayah=..., nama_ibu=...
WHERE nis='NIS004'
```

### DELETE:
```sql
DELETE FROM tb_siswa WHERE nis='NIS004'
-- User di tb_users otomatis terhapus (ON DELETE CASCADE)
```

---

## 🧪 TESTING

### Test 1: Load Kelas di ComboBox
1. Jalankan aplikasi
2. Buka FormDataSiswa
3. Klik ComboBox1
4. **Expected:** Muncul list kelas (X-1, X-2, XI-1, dst) ✅

### Test 2: Simpan Siswa Baru
1. Isi TextBox1 (NIS): `NIS999`
2. Isi TextBox2 (Nama): `Test Siswa`
3. Pilih RadioButton1 (Laki-Laki)
4. Isi TextBox3 (TTL): `2008-01-01`
5. Pilih ComboBox1: `X-1`
6. Isi alamat, ayah, ibu
7. Klik Button1 (Simpan)
8. **Expected:** 
   - Muncul pesan: "Username: NIS999, Password: Test"
   - Data muncul di DataGridView
   - Bisa login dengan NIS999/Test ✅

### Test 3: ComboBox Kelas
1. Klik baris siswa di DataGridView
2. **Expected:** ComboBox1 terisi dengan nama kelas siswa ✅

### Test 4: Edit Siswa (Ganti Kelas)
1. Klik baris di DataGridView
2. Ubah ComboBox1 dari "X-1" ke "XI-1"
3. Klik Button2 (Edit)
4. **Expected:** Data terupdate, kelas berubah ✅

### Test 5: Search
1. Ketik "NIS999" di TextBox8
2. Klik Button6 (Cari)
3. **Expected:** DataGridView filter siswa dengan NIS999 ✅

---

## ⚠️ VALIDASI

### NIS & Nama (Required):
```vb
If TextBox1.Text = "" Or TextBox2.Text = "" Then
    MsgBox("NIS dan Nama harus diisi!")
    Return
End If
```

### Jenis Kelamin (Required):
```vb
If Not RadioButton1.Checked And Not RadioButton2.Checked Then
    MsgBox("Pilih jenis kelamin!")
    Return
End If
```

### Kelas (Required):
```vb
If ComboBox1.SelectedIndex = -1 Then
    MsgBox("Pilih kelas!")
    Return
End If
```

---

## ✅ STATUS

- [x] Koneksi database
- [x] Load ComboBox kelas dari tb_kelas
- [x] TampilData dengan JOIN ke tb_kelas
- [x] Button Simpan → sp_insert_siswa (auto create user)
- [x] Button Edit → UPDATE tb_siswa
- [x] Button Hapus → DELETE (CASCADE ke user)
- [x] Button Cancel → Bersih form
- [x] Button Close → Tutup form
- [x] Button Cari → Search NIS/Nama
- [x] DataGridView Click → Fill form (termasuk ComboBox)
- [x] GetIdKelas() untuk konversi nama → id
- [x] Validasi input lengkap
- [x] Auto create user dengan username=NIS, password=nama depan

**FormDataSiswa READY dengan ComboBox Kelas!** 🎉
