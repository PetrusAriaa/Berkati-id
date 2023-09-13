﻿namespace Berkati_Backend.Models
{
    public class UserRequests
    {
        private string _id;
        private string _userId;
        private DateTime tanggal;
        private string jalan;
        private string rt;
        private string rw;
        private string kel;
        private string kec;
        private string kab_kota;
        private string provinsi;
        private string kodepos;
        private UInt32 est_jumlah;
        private bool status;

        public string Id { get => _id; set => _id = value; }
        public string UserId { get => _userId; set => _userId = value; }
        public DateTime Tanggal { get => tanggal; set => tanggal = value; }
        public string Jalan { get => jalan; set => jalan = value; }
        public string Rt { get => rt; set => rt = value; }
        public string Rw { get => rw; set => rw = value; }
        public string Kel { get => kel; set => kel = value; }
        public string Kec { get => kec; set => kec = value; }
        public string Kab_kota { get => kab_kota; set => kab_kota = value; }
        public string Provinsi { get => provinsi; set => provinsi = value; }
        public string Kodepos { get => kodepos; set => kodepos = value; }
        public uint Est_jumlah { get => est_jumlah; set => est_jumlah = value; }
        public bool Status { get => status; set => status = value; }

        public UserRequests()
        {

        }
    }
}
