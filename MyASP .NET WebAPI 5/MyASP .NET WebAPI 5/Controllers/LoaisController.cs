using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyASP_.NET_WebAPI_5.Data;
using MyASP_.NET_WebAPI_5.Models;
using System;
using System.Linq;

namespace MyASP_.NET_WebAPI_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaisController : ControllerBase // là 1 instance class trong .ASP NET dùng để xử lý các yêu cầu HTTP và phản hồi
    {
        private readonly MyDbContext _context;

        public LoaisController(MyDbContext context) { 
            _context = context;

        }

        [HttpGet]
        public IActionResult GetAll() // IActionResult là kiểu của dữ liệu trả về 
        {   
            var dsLoai = _context.Loais.ToList();
            return Ok(dsLoai);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var loai = _context.Loais.SingleOrDefault(type => type.MaLoai == id);
            if(loai != null)
            {
                return Ok(loai);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        public IActionResult CreateNew(LoaiModel model)
        {
            try
            {
                var loai = new Loai
                {
                    TenLoai = model.TenLoai
                };
                _context.Add(loai);
                _context.SaveChanges();
                //return Ok(loai);
                return StatusCode(StatusCodes.Status201Created, loai);
            }catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateById(int id, LoaiModel model)
        {
            var loai = _context.Loais.SingleOrDefault(type => type.MaLoai == id);
            if (loai != null)
            {
                loai.TenLoai = model.TenLoai;
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }

        }


        [HttpDelete("{id}")]
        public IActionResult DeleteLoaiById(int id)
        {
            var loai = _context.Loais.SingleOrDefault(type => type.MaLoai == id);
            if (loai != null)
            {
                _context.Remove(loai);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return NotFound();
            }

        }
    }
}
