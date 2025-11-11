# 🔧 FIX: Unable to Convert MySQL Date/Time Value

## ❌ MASALAH:

**Error:**
```
Unable to convert MySQL date/time value to System.DateTime
```

**Penyebab:**
- Field `tanggal_lahir` di database bertipe `DATE` (format: YYYY-MM-DD)
- User input format: **"Kediri, 09 Agustus 2006"** (STRING)
- Tidak bisa dikonversi otomatis dari STRING → DATE

---

## ✅ SOLUSI:

Ubah field `tanggal_lahir` dari `DATE` menjadi `VARCHAR(100)` agar bisa menyimpan format lengkap **"Kota, DD Bulan YYYY"**

---

## 🔨 CARA PERBAIKAN:

### **Opsi 1: Import Database Baru (RECOMMENDED)**

Jika belum punya banyak data, import ulang database yang sudah diperbaiki:

```bash
# Drop database lama (HATI-HATI! Data akan hilang)
DROP DATABASE db_sekolah;

# Import database baru
mysql -u root < "d:\SEMESTER 3\Pemrograman visual\UTS\PROJECT PEMVIS\db_sekolah_with_login.sql"
```

### **Opsi 2: Update Database Existing**

Jika sudah punya data dan tidak mau hilang, jalankan script UPDATE:

```bash
mysql -u root db_sekolah < "d:\SEMESTER 3\Pemrograman visual\UTS\PROJECT PEMVIS\UPDATE_TTL_FORMAT.sql"
```

Atau via phpMyAdmin:
1. Buka http://localhost/phpmyadmin
2. Pilih database `db_sekolah`
3. Klik tab SQL
4. Copy-paste isi file `UPDATE_TTL_FORMAT.sql`
5. Klik Go

---

## 📊 PERUBAHAN DATABASE:

### **SEBELUM (❌):**
```sql
CREATE TABLE tb_siswa (
    ...
    tanggal_lahir DATE,  -- Hanya bisa: '2008-05-15'
    ...
);
```

### **SESUDAH (✅):**
```sql
CREATE TABLE tb_siswa (
    ...
    tanggal_lahir VARCHAR(100),  -- Bisa: 'Kediri, 09 Agustus 2006'
    ...
);
```

---

## 📝 FORMAT TTL YANG BENAR:

### ✅ Format yang Diterima:
```
"Kediri, 09 Agustus 2006"
"Jakarta, 15 Mei 2008"
"Surabaya, 20 Desember 2007"
"Bandung, 01 Januari 2009"
```

### ❌ Format yang SALAH:
```
"2008-05-15"           ❌ (ini format DATE, bukan STRING)
"Kediri 09 Agustus"    ❌ (tidak ada koma dan tahun)
"09/08/2006"           ❌ (format angka)
```

### 💡 Tips Format:
- **Tempat:** Nama kota/kabupaten
- **Koma:** Wajib ada setelah tempat
- **Spasi:** Setelah koma
- **Tanggal:** DD (2 digit)
- **Bulan:** Nama bulan lengkap (Januari, Februari, dst)
- **Tahun:** YYYY (4 digit)

---

## 🖥️ PENGGUNAAN DI FORM:

### FormDataSiswa - TextBox3 (TTL):

**User isi:**
```
TextBox1 (NIS):    NIS004
TextBox2 (Nama):   Andi Pratama
RadioButton1:      Laki-Laki ✓
TextBox3 (TTL):    Kediri, 09 Agustus 2006  ← Format ini!
ComboBox1:         X-1
TextBox5 (Alamat): Jl. Sudirman
...
```

**Klik Simpan → System:**
```vb
cmd.Parameters.AddWithValue("@p_tanggal_lahir", TextBox3.Text)
' Value: "Kediri, 09 Agustus 2006"
```

**Tersimpan di database:**
```sql
tanggal_lahir = 'Kediri, 09 Agustus 2006'
```

**Tampil di DataGridView:**
```
Column TTL: Kediri, 09 Agustus 2006
```

✅ **TIDAK ADA ERROR!**

---

## 🧪 TESTING:

### Test 1: Simpan Siswa Baru
```
1. Buka FormDataSiswa
2. Isi TextBox3: "Jakarta, 15 Mei 2008"
3. Isi field lainnya
4. Klik Simpan
5. Expected: Data tersimpan tanpa error ✅
```

### Test 2: Edit Siswa
```
1. Klik baris siswa di DataGridView
2. TextBox3 terisi: "Jakarta, 15 Mei 2008"
3. Ubah: "Bandung, 20 Juni 2008"
4. Klik Edit
5. Expected: Data terupdate tanpa error ✅
```

### Test 3: Tampil di DataGridView
```
1. Load FormDataSiswa
2. DataGridView column TTL menampilkan: "Jakarta, 15 Mei 2008"
3. Expected: Tidak ada error convert date ✅
```

---

## 📋 STORED PROCEDURE:

### **sp_insert_siswa (Updated):**

```sql
CREATE PROCEDURE sp_insert_siswa(
    IN p_nis VARCHAR(50),
    IN p_nama_lengkap VARCHAR(100),
    IN p_jenis_kelamin VARCHAR(20),
    IN p_tanggal_lahir VARCHAR(100),  -- ✅ VARCHAR, bukan DATE!
    IN p_id_kelas INT,
    IN p_alamat TEXT,
    IN p_nama_ayah VARCHAR(100),
    IN p_nama_ibu VARCHAR(100)
)
```

**Cara Pakai:**
```sql
CALL sp_insert_siswa(
    'NIS004',                      -- NIS
    'Andi Pratama',                -- Nama
    'Laki-Laki',                   -- Gender
    'Kediri, 09 Agustus 2006',     -- TTL ✅
    1,                             -- ID Kelas
    'Jl. Sudirman',                -- Alamat
    'Bapak Andi',                  -- Ayah
    'Ibu Andi'                     -- Ibu
);
```

---

## 🎯 SAMPLE DATA (Updated):

```sql
-- Siswa 1
INSERT INTO tb_siswa (..., tanggal_lahir, ...) VALUES
(..., 'Jakarta, 15 Mei 2008', ...);

-- Siswa 2
INSERT INTO tb_siswa (..., tanggal_lahir, ...) VALUES
(..., 'Bandung, 20 Agustus 2008', ...);

-- Siswa 3
INSERT INTO tb_siswa (..., tanggal_lahir, ...) VALUES
(..., 'Surabaya, 10 Maret 2008', ...);
```

---

## 📌 CATATAN PENTING:

### ⚠️ Jika Sudah Input Data dengan Format Lama:

Jika sudah ada data siswa dengan format `2008-05-15`, bisa dikonversi manual:

```sql
-- Update satu per satu
UPDATE tb_siswa 
SET tanggal_lahir = 'Jakarta, 15 Mei 2008' 
WHERE nis = 'NIS001';

UPDATE tb_siswa 
SET tanggal_lahir = 'Bandung, 20 Agustus 2008' 
WHERE nis = 'NIS002';
```

### 🔄 Auto Convert (Tidak Recommended):

Script ini akan convert otomatis tapi **kehilangan info tempat lahir**:

```sql
UPDATE tb_siswa 
SET tanggal_lahir = CONCAT(
    'Unknown, ',
    DATE_FORMAT(STR_TO_DATE(tanggal_lahir, '%Y-%m-%d'), '%d %M %Y')
)
WHERE tanggal_lahir LIKE '____-__-__';
```

Hasil:
- `2008-05-15` → `Unknown, 15 May 2008`

---

## ✅ CHECKLIST:

- [ ] Database diupdate (tanggal_lahir → VARCHAR(100))
- [ ] Stored procedure sp_insert_siswa diupdate
- [ ] Sample data diupdate dengan format baru
- [ ] FormDataSiswa.vb tetap sama (sudah pakai string)
- [ ] Test simpan siswa baru
- [ ] Test edit siswa
- [ ] Test tampil di DataGridView

---

## 🎉 KESIMPULAN:

### Perubahan:
✅ **tanggal_lahir:** DATE → VARCHAR(100)  
✅ **Format:** "Kota, DD Bulan YYYY"  
✅ **Stored Procedure:** Parameter updated  
✅ **Sample Data:** Format updated  

### Keuntungan:
- ✅ Tidak ada error convert date lagi
- ✅ Bisa simpan format lengkap dengan tempat lahir
- ✅ User-friendly (format Indonesia)
- ✅ Fleksibel (bisa isi format apapun)

### Kerugian:
- ⚠️ Tidak bisa sorting by date (karena string)
- ⚠️ Tidak bisa hitung umur otomatis
- ⚠️ Tidak ada validasi format

**TAPI untuk keperluan sistem sekolah sederhana, format STRING lebih praktis!** 🎉

---

## 📞 TROUBLESHOOTING:

### Error: "Procedure sp_insert_siswa does not exist"
**Solusi:** Jalankan `UPDATE_TTL_FORMAT.sql` untuk buat ulang procedure

### Error: "Data too long for column tanggal_lahir"
**Solusi:** Pastikan VARCHAR(100) sudah cukup panjang

### Data siswa lama masih format DATE
**Solusi:** Update manual satu per satu atau pakai script convert

---

**FIX COMPLETE! TTL sekarang pakai format STRING!** ✅
