using System.Collections.Generic;
using System.Linq;

namespace SistemDataMahasiswa
{
    public class MahasiswaService
    {
        private List<Mahasiswa> _gudangData;

        public MahasiswaService()
        {
            _gudangData = new List<Mahasiswa>();
        }

        public void RegistrasiDataBaru(Mahasiswa entitasBaru)
        {
            _gudangData.Add(entitasBaru);
        }

        public bool CekNrpTersedia(string nrp)
        {
            return _gudangData.Any(m => m.NRP == nrp);
        }

        public void HapusBerdasarkanNRP(string nrp)
        {
            var targetData = _gudangData.FirstOrDefault(m => m.NRP == nrp);
            if (targetData != null)
            {
                _gudangData.Remove(targetData);
            }
        }

        public void PerbaruiData(Mahasiswa dataUpdate)
        {
            var targetData = _gudangData.FirstOrDefault(m => m.NRP == dataUpdate.NRP);
            if (targetData != null)
            {
                targetData.NamaLengkap = dataUpdate.NamaLengkap;
                targetData.ProgramStudi = dataUpdate.ProgramStudi;
                targetData.IPK = dataUpdate.IPK;
            }
        }

        public List<Mahasiswa> DapatkanSemuaArsip()
        {
            return _gudangData.ToList();
        }

        public List<Mahasiswa> SaringData(string kataKunci)
        {
            if (string.IsNullOrWhiteSpace(kataKunci)) 
                return DapatkanSemuaArsip();

            kataKunci = kataKunci.ToLower();
            return _gudangData.Where(m => 
                m.NRP.ToLower().Contains(kataKunci) || 
                m.NamaLengkap.ToLower().Contains(kataKunci)
            ).ToList();
        }
    }
}