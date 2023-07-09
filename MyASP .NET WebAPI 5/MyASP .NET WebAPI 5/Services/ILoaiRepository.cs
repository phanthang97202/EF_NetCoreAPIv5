using MyASP_.NET_WebAPI_5.Models;
using System.Collections.Generic;

namespace MyASP_.NET_WebAPI_5.Services
{
    public interface ILoaiRepository
    {
        List<LoaiVM> GetAll();
        LoaiVM GetById(int id);
        LoaiVM Add(LoaiModel loai);
        void Update(LoaiVM loai);
        void Delete(int id);


    }
}
