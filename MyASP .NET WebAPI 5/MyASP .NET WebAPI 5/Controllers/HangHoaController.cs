using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyASP_.NET_WebAPI_5.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyASP_.NET_WebAPI_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase

    {
        public static List<HangHoa> hangHoas = new List<HangHoa>();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(hangHoas);

        }

        [HttpPost]

        public IActionResult Create(HangHoaVM hangHoaVM)
        {
            var hanghoa = new HangHoa
            {
                MaHangHoa = Guid.NewGuid(),
                TenHangHoa = hangHoaVM.TenHangHoa,
                DonGia = hangHoaVM.DonGia
            };



            hangHoas.Add(hanghoa);

            return Ok(new
            {
                Success = true,
                Data = hanghoa
            });
        }


        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            // LINQ Query
            try
            {

                var hangHoa = hangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (hangHoa == null)
                {
                    return NotFound();

                }
                return Ok(new
                {
                    Success = true,
                    Data = hangHoa

                });
            }
            catch (Exception exeption)
            {

                return BadRequest(exeption);
            }
        }


        [HttpPut("{id}")]
        public IActionResult Edit(string id, HangHoa hangHoaEdit)
        {
            // LINQ Query
            try
            {

                var hangHoa = hangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (hangHoa == null)
                {
                    return NotFound();

                }

                //chekc thêm cả IDHangHoa và MaHangHoa 
                if (id != hangHoa.MaHangHoa.ToString())
                {
                    return BadRequest();
                }

                // Thực hiện update id hàng hóa
                hangHoa.TenHangHoa = hangHoaEdit.TenHangHoa;
                hangHoa.DonGia = hangHoaEdit.DonGia;

                return Ok(new
                {
                    Success = true,
                    Data = hangHoa

                });
            }
            catch (Exception exeption)
            {

                return BadRequest(exeption);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(string id )
        {
            // LINQ Query
            try
            {

                var hangHoa = hangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (hangHoa == null)
                {
                    return NotFound();

                } 

                // Thực hiện xóa id hàng hóa 
                hangHoas.Remove(hangHoa);

                return Ok(new
                {
                    Success = true,
                    Data = hangHoa

                });
            }
            catch (Exception exeption)
            {

                return BadRequest(exeption);
            }
        }

    }
}
