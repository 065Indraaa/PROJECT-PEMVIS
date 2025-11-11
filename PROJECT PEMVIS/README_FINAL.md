# 🎓 SISTEM MANAJEMEN SEKOLAH - FINAL VERSION

## 📦 FILE YANG SUDAH DIBUAT

### 1. **Database SQL** 🗄️
- ✅ `db_sekolah_with_login.sql` - Database lengkap dengan sistem login & role
- ✅ `db_sekolah_4form.sql` - Database tanpa login (versi sederhana)

### 2. **VB.NET Forms** 💻
- ✅ `FormLogin.vb` - Form login dengan role-based authentication
- ✅ `FormDataGuru.vb` - CRUD Guru dengan auto create user

### 3. **Dokumentasi** 📚
- ✅ `PANDUAN_LOGIN_SYSTEM.md` - Panduan lengkap sistem login
- ✅ `LOGIN_CREDENTIALS.txt` - Quick reference credentials
- ✅ `PANDUAN_4FORM.md` - Panduan 4 form utama
- ✅ `TEST_KONEKSI.md` - Checklist koneksi database
- ✅ `ALUR_DATA_GURU.txt` - Diagram alur data
- ✅ `README_FINAL.md` - File ini

---

## 🚀 QUICK START

### Step 1: Import Database
```bash
mysql -u root < "d:\SEMESTER 3\Pemrograman visual\UTS\PROJECT PEMVIS\db_sekolah_with_login.sql"
```

Atau via phpMyAdmin:
1. Buka http://localhost/phpmyadmin
2. Import → Pilih `db_sekolah_with_login.sql`
3. Klik Go

### Step 2: Jalankan Aplikasi
1. Build & Run VB.NET Project
2. FormLogin akan muncul
3. Login dengan credentials:
   - **Admin:** username=`admin`, password=`admin123`
   - **Guru:** username=`NIP001`, password=`Budi`
   - **Siswa:** username=`NIS001`, password=`Ahmad`

---

## 🎯 FITUR UTAMA

### ✅ 1. Sistem Login dengan Role
- Admin → Akses FormAdmin
- Guru → Akses FormGuru
- Siswa → Akses FormSiswa

### ✅ 2. Auto Create User
Saat input Guru/Siswa baru:
- Username otomatis = NIP/NIS
- Password otomatis = Nama Depan
- Bisa langsung login!

### ✅ 3. CRUD Lengkap (FormDataGuru)
- ✅ **Create:** Simpan data + auto create user
- ✅ **Read:** Tampil data di DataGridView
- ✅ **Update:** Edit data guru
- ✅ **Delete:** Hapus data guru (+ user otomatis terhapus)
- ✅ **Search:** Cari berdasarkan NIP/Nama

---

## 📊 STRUKTUR DATABASE

```
db_sekolah
├── tb_role (Admin, Guru, Siswa)
├── tb_users (Username, Password, Role) 🔑
├── tb_guru (Data Guru + FK→tb_users)
├── tb_siswa (Data Siswa + FK→tb_users)
├── tb_kelas (Data Kelas)
├── tb_mata_pelajaran (Data Mapel)
├── v_guru_lengkap (View dengan JOIN)
├── v_siswa_lengkap (View dengan JOIN)
├── v_mapel_lengkap (View dengan JOIN)
├── sp_insert_guru (Stored Procedure) 🔧
└── sp_insert_siswa (Stored Procedure) 🔧
```

---

## 🔐 LOGIN CREDENTIALS

### Default Accounts:

| Role  | Username | Password  | Nama Lengkap        | Form Target |
|-------|----------|-----------|---------------------|-------------|
| Admin | admin    | admin123  | Administrator       | FormAdmin   |
| Guru  | NIP001   | Budi      | Budi Santoso, S.Pd  | FormGuru    |
| Guru  | NIP002   | Siti      | Siti Aminah, S.Pd   | FormGuru    |
| Siswa | NIS001   | Ahmad     | Ahmad Rizki         | FormSiswa   |
| Siswa | NIS002   | Dewi      | Dewi Lestari        | FormSiswa   |
| Siswa | NIS003   | Andi      | Andi Pratama        | FormSiswa   |

---

## 💡 CARA KERJA AUTO CREATE USER

### Scenario: Input Guru Baru

**User mengisi form di FormDataGuru:**
```
NIP           : NIP999
Nama Lengkap  : John Doe, S.Pd
Mata Pelajaran: Fisika
Jenis Kelamin : Laki-Laki
Alamat        : Jl. Merdeka No. 5
No Telepon    : 08123456789
```

**Klik Button Simpan → System otomatis:**
1. Insert ke `tb_users`:
   - username = `NIP999`
   - password = `John` (nama depan)
   - id_role = `2` (Guru)
   - status = `Aktif`

2. Insert ke `tb_guru`:
   - id_user = (dari step 1)
   - nip = `NIP999`
   - nama_lengkap = `John Doe, S.Pd`
   - dst...

**Muncul pesan:**
```
✅ Data Berhasil Disimpan!
   Username: NIP999
   Password: John
```

**Guru bisa langsung login:**
```
Username: NIP999
Password: John
→ Redirect ke FormGuru
```

---

## 🧪 TESTING CHECKLIST

### Test 1: Login
- [ ] Login sebagai Admin (admin/admin123)
- [ ] Login sebagai Guru (NIP001/Budi)
- [ ] Login sebagai Siswa (NIS001/Ahmad)
- [ ] Coba login dengan password salah (harus gagal)

### Test 2: CRUD Guru
- [ ] Simpan guru baru → Cek muncul di DataGridView
- [ ] Cek dapat info Username & Password
- [ ] Klik baris di DataGridView → Form terisi
- [ ] Edit data guru → Klik Edit
- [ ] Hapus guru → Konfirmasi Yes
- [ ] Cari guru berdasarkan NIP/Nama

### Test 3: Auto Create User
- [ ] Input guru baru dengan NIP=NIP888
- [ ] Simpan
- [ ] Logout
- [ ] Login dengan username=NIP888
- [ ] Harus berhasil masuk ke FormGuru

---

## 🔧 KONEKSI DATABASE

### Connection String:
```vb
server=localhost;user=root;password=;database=db_sekolah
```

### Jika MySQL di port 3307:
```vb
server=localhost;port=3307;user=root;password=;database=db_sekolah
```

### Jika ada password:
```vb
server=localhost;user=root;password=yourpass;database=db_sekolah
```

---

## 📂 FILE MAPPING

### Untuk FormDataGuru:
| Form Control | Database Column | Tabel |
|--------------|-----------------|-------|
| TextBox1 | nip | tb_guru |
| TextBox2 | nama_lengkap | tb_guru |
| TextBox4 | mata_pelajaran | tb_guru |
| RadioButton1/2 | jenis_kelamin | tb_guru |
| TextBox3 | alamat | tb_guru |
| TextBox5 | no_telepon | tb_guru |
| TextBox8 | - | (search) |

### DataGridView Columns:
- Column 0: KodeGuru (nip)
- Column 1: NamaGuru (nama_lengkap)
- Column 2: Mapel (mata_pelajaran)
- Column 3: JenisKelamin (jenis_kelamin)
- Column 4: Alamat (alamat)
- Column 5: NoTlpn (no_telepon)

---

## ⚠️ TROUBLESHOOTING

### Problem: "Koneksi Gagal"
**Solution:**
- Cek MySQL service running
- Cek port (3306 atau 3307)
- Cek username/password
- Pastikan database `db_sekolah` exists

### Problem: "Login Gagal"
**Solution:**
- Username & Password case-sensitive
- Cek status user (harus 'Aktif')
- Pastikan data ada di tb_users
- Cek query di FormLogin.vb

### Problem: "Stored procedure not found"
**Solution:**
- Import ulang `db_sekolah_with_login.sql`
- Cek: `SHOW PROCEDURE STATUS WHERE Db = 'db_sekolah';`
- Pastikan sp_insert_guru dan sp_insert_siswa ada

### Problem: "Data tidak muncul di DataGridView"
**Solution:**
- Cek view: `SELECT * FROM v_guru_lengkap;`
- Cek TampilData() dipanggil di Form_Load
- Cek koneksi database berhasil

---

## 📝 NEXT STEPS

### Yang Sudah Selesai: ✅
- [x] Database dengan login & role
- [x] Stored procedures untuk auto create user
- [x] FormLogin dengan role-based redirect
- [x] FormDataGuru dengan CRUD lengkap
- [x] Auto create user saat simpan guru
- [x] Views untuk JOIN data
- [x] Dokumentasi lengkap

### Yang Perlu Dilengkapi: 📋
- [ ] FormDataSiswa dengan CRUD lengkap (ikuti pola FormDataGuru)
- [ ] DataKelas dengan CRUD lengkap
- [ ] FormDataMapel dengan CRUD lengkap
- [ ] FormGuru (tampilan untuk guru yang login)
- [ ] FormSiswa (tampilan untuk siswa yang login)

**Pola sudah ada di FormDataGuru.vb! Tinggal copy dan sesuaikan!** 💡

---

## 🎉 SUMMARY

### ✅ SISTEM SUDAH READY:
1. **Database** dengan login system & role
2. **FormLogin** dengan authentication
3. **FormDataGuru** dengan CRUD + auto create user
4. **Stored Procedures** untuk auto create user
5. **Views** untuk JOIN data
6. **Dokumentasi** lengkap

### 🔑 KEY FEATURES:
- Auto create user saat input guru/siswa
- Username = NIP/NIS
- Password = Nama Depan
- Role-based login (Admin/Guru/Siswa)
- CRUD lengkap dengan validasi

### 📚 DOKUMENTASI:
- PANDUAN_LOGIN_SYSTEM.md
- LOGIN_CREDENTIALS.txt
- PANDUAN_4FORM.md
- TEST_KONEKSI.md
- ALUR_DATA_GURU.txt

---

## 🚀 READY TO USE!

**Import database → Run aplikasi → Login → Test CRUD!** 🎉

**Semua file sudah lengkap dan siap digunakan!**
