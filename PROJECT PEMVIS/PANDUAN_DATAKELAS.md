# 📚 PANDUAN DATA KELAS

## 🎯 FITUR LENGKAP

### ✅ CRUD Operations:
- **Create:** Tambah kelas baru
- **Read:** Tampil data di DataGridView
- **Update:** Edit data kelas
- **Delete:** Hapus data kelas

---

## 📊 MAPPING: Form ↔ Database

### Input Controls:
| Form Control | Type | Database Column | Nilai |
|--------------|------|-----------------|-------|
| `namakls` | TextBox | `nama_kelas` | Contoh: X-1, XI-2, XII-3 |
| `TextBox2` | TextBox | `wali_kelas` | Nama wali kelas |
| `HScrollBar1` | HScrollBar | `kapasitas` | 10-50 siswa |
| `Label5` | Label | - | Menampilkan nilai kapasitas |

### DataGridView Columns:
| Column | Index | Database Column | Deskripsi |
|--------|-------|-----------------|-----------|
| Namakelas | 0 | `nama_kelas` | Nama kelas (X-1, XI-2, dll) |
| Walikelas | 1 | `wali_kelas` | Nama wali kelas |
| Kapasitas | 2 | `kapasitas` | Jumlah kapasitas siswa |

### Buttons:
| Button | Fungsi | Keterangan |
|--------|--------|------------|
| Button1 | Tambah | Simpan kelas baru ke database |
| Button2 | Edit | Update data kelas yang dipilih |
| Button3 | Hapus | Hapus data kelas yang dipilih |

---

## 🔢 NUMERIC INPUT (HScrollBar)

### HScrollBar untuk Kapasitas:
```vb
HScrollBar1.Minimum = 10  ' Kapasitas minimal
HScrollBar1.Maximum = 50  ' Kapasitas maksimal
HScrollBar1.Value = 30    ' Default value
```

### Label5 Display:
- Menampilkan nilai real-time dari HScrollBar
- Update otomatis saat user scroll
- Format: angka (contoh: "30")

---

## 💻 IMPLEMENTASI VB.NET

### 1. Koneksi Database
```vb
Sub koneksi()
    conn = New MySqlConnection("server=localhost;user=root;password=;database=db_sekolah")
    conn.Open()
End Sub
```

### 2. Tampil Data (Form Load)
```vb
Private Sub DataKelas_Load(...) Handles MyBase.Load
    TampilData()  ' Load data ke DataGridView
    HScrollBar1.Minimum = 10
    HScrollBar1.Maximum = 50
    HScrollBar1.Value = 30
    Label5.Text = "30"
End Sub
```

### 3. HScrollBar Event
```vb
Private Sub HScrollBar1_Scroll(...) Handles HScrollBar1.Scroll
    Label5.Text = HScrollBar1.Value.ToString()
End Sub
```

### 4. Button Tambah
```vb
Private Sub Button1_Click(...) Handles Button1.Click
    ' Validasi
    If namakls.Text = "" Then
        MsgBox("Nama Kelas harus diisi!")
        Return
    End If
    
    ' INSERT ke database
    koneksi()
    cmd = New MySqlCommand("INSERT INTO tb_kelas (nama_kelas, wali_kelas, kapasitas) VALUES (@nama, @wali, @kapasitas)", conn)
    cmd.Parameters.AddWithValue("@nama", namakls.Text)
    cmd.Parameters.AddWithValue("@wali", TextBox2.Text)
    cmd.Parameters.AddWithValue("@kapasitas", HScrollBar1.Value)
    cmd.ExecuteNonQuery()
    
    MsgBox("Data Berhasil Disimpan!")
    Bersih()
    TampilData()
End Sub
```

### 5. Button Edit
```vb
Private Sub Button2_Click(...) Handles Button2.Click
    ' UPDATE database
    cmd = New MySqlCommand("UPDATE tb_kelas SET wali_kelas=@wali, kapasitas=@kapasitas WHERE nama_kelas=@nama", conn)
    cmd.Parameters.AddWithValue("@nama", namakls.Text)
    cmd.Parameters.AddWithValue("@wali", TextBox2.Text)
    cmd.Parameters.AddWithValue("@kapasitas", HScrollBar1.Value)
    cmd.ExecuteNonQuery()
    
    MsgBox("Data Berhasil Diupdate!")
End Sub
```

### 6. Button Hapus
```vb
Private Sub Button3_Click(...) Handles Button3.Click
    If MsgBox("Yakin hapus data?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        ' DELETE dari database
        cmd = New MySqlCommand("DELETE FROM tb_kelas WHERE nama_kelas=@nama", conn)
        cmd.Parameters.AddWithValue("@nama", namakls.Text)
        cmd.ExecuteNonQuery()
        
        MsgBox("Data Berhasil Dihapus!")
    End If
End Sub
```

### 7. DataGridView Click
```vb
Private Sub DataGridView1_CellClick(...) Handles DataGridView1.CellClick
    If e.RowIndex >= 0 Then
        Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        namakls.Text = row.Cells(0).Value.ToString()
        TextBox2.Text = row.Cells(1).Value.ToString()
        HScrollBar1.Value = Convert.ToInt32(row.Cells(2).Value)
        Label5.Text = HScrollBar1.Value.ToString()
    End If
End Sub
```

---

## 🔄 ALUR KERJA

### 1. Tambah Kelas Baru:
```
User mengisi form:
  namakls:     "XII-3"
  TextBox2:    "Ahmad Santoso, S.Pd"
  HScrollBar1: 35 (geser scrollbar)
  Label5:      "35" (otomatis update)

↓ Klik Button1 (Tambah)

System:
  1. Validasi namakls tidak kosong
  2. INSERT INTO tb_kelas
     (nama_kelas, wali_kelas, kapasitas)
     VALUES ('XII-3', 'Ahmad Santoso, S.Pd', 35)
  3. Tampil "Data Berhasil Disimpan!"
  4. Bersihkan form
  5. Refresh DataGridView

✅ Kelas baru muncul di DataGridView
```

### 2. Edit Kelas:
```
User klik baris di DataGridView:
  ↓ Form terisi otomatis
  namakls:     "XII-3"
  TextBox2:    "Ahmad Santoso, S.Pd"
  HScrollBar1: 35
  Label5:      "35"

User ubah:
  TextBox2:    "Budi Raharjo, S.Pd"
  HScrollBar1: 40
  Label5:      "40"

↓ Klik Button2 (Edit)

System:
  1. UPDATE tb_kelas
     SET wali_kelas='Budi Raharjo, S.Pd', kapasitas=40
     WHERE nama_kelas='XII-3'
  2. Tampil "Data Berhasil Diupdate!"
  3. Refresh DataGridView

✅ Data ter-update di DataGridView
```

### 3. Hapus Kelas:
```
User klik baris di DataGridView
  ↓ Form terisi otomatis

↓ Klik Button3 (Hapus)
↓ MsgBox "Yakin hapus data?"
↓ User klik Yes

System:
  1. DELETE FROM tb_kelas WHERE nama_kelas='XII-3'
  2. Tampil "Data Berhasil Dihapus!"
  3. Refresh DataGridView

✅ Data terhapus dari DataGridView
```

---

## 📋 DATABASE QUERY

### SELECT (Tampil Data):
```sql
SELECT nama_kelas, wali_kelas, kapasitas 
FROM tb_kelas 
ORDER BY id_kelas
```

### INSERT (Tambah):
```sql
INSERT INTO tb_kelas (nama_kelas, wali_kelas, kapasitas) 
VALUES ('XII-3', 'Ahmad Santoso, S.Pd', 35)
```

### UPDATE (Edit):
```sql
UPDATE tb_kelas 
SET wali_kelas='Budi Raharjo, S.Pd', kapasitas=40 
WHERE nama_kelas='XII-3'
```

### DELETE (Hapus):
```sql
DELETE FROM tb_kelas 
WHERE nama_kelas='XII-3'
```

---

## 🧪 TESTING

### Test 1: Tambah Kelas
1. Jalankan aplikasi
2. Buka DataKelas form
3. Isi namakls: `XII-3`
4. Isi TextBox2: `Test Wali, S.Pd`
5. Geser HScrollBar ke 35
6. Cek Label5 = `35`
7. Klik Button1 (Tambah)
8. **Expected:** Data muncul di DataGridView ✅

### Test 2: HScrollBar
1. Geser HScrollBar dari 10 ke 50
2. **Expected:** Label5 update real-time (10...20...30...40...50) ✅

### Test 3: DataGridView Click
1. Klik baris pertama di DataGridView
2. **Expected:** 
   - namakls terisi
   - TextBox2 terisi
   - HScrollBar value sesuai kapasitas
   - Label5 tampil nilai kapasitas ✅

### Test 4: Edit Kelas
1. Klik baris di DataGridView
2. Ubah TextBox2 dan HScrollBar
3. Klik Button2 (Edit)
4. **Expected:** Data terupdate di DataGridView ✅

### Test 5: Hapus Kelas
1. Klik baris di DataGridView
2. Klik Button3 (Hapus)
3. Klik Yes
4. **Expected:** Data terhapus dari DataGridView ✅

---

## ⚠️ VALIDASI

### Nama Kelas (Required):
```vb
If namakls.Text = "" Then
    MsgBox("Nama Kelas harus diisi!")
    Return
End If
```

### Kapasitas Range:
```vb
HScrollBar1.Minimum = 10  ' Min 10 siswa
HScrollBar1.Maximum = 50  ' Max 50 siswa
```

### Konfirmasi Hapus:
```vb
If MsgBox("Yakin hapus data?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
    ' Hapus data
End If
```

---

## ✅ STATUS

- [x] Koneksi database
- [x] TampilData (Form Load)
- [x] HScrollBar untuk numeric input (10-50)
- [x] Label5 untuk display nilai
- [x] Button Tambah → INSERT
- [x] Button Edit → UPDATE
- [x] Button Hapus → DELETE
- [x] DataGridView Click → Isi form
- [x] Validasi input
- [x] Konfirmasi hapus
- [x] Bersih form setelah operasi

**DataKelas SIAP DIGUNAKAN!** 🎉
