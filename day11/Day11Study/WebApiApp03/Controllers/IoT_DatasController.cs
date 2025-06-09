using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiApp03.Models;

namespace WebApiApp03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IoT_DatasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IoT_DatasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/IoT_Datas
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<iot_datas>>> GetIoT_Datas()
        //{
        //    return await _context.iot_datas.ToListAsync();
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<iot_datas>>> GetIoT_Datas(string serviceKey, string startDate, string endDate, string resultType)
        {
            // 실제 데이터포털(data.go.kr)에서 사용하는 방법
            // 1. 서비스키가 일치하는 요청만 수행
            if (string.IsNullOrWhiteSpace(serviceKey)) 
            {
                return BadRequest();
            }
            else
            {
                // 서버에서 키를 검색해서 검증된 키인지 확인하고 맞으면 진행
            }

            // 2. pageNo, numOfRows 파라미터가 있으면, 실제 데이터를 페이징해서 데이터를 돌려받음
            Debug.WriteLine(startDate, endDate);
            var result = await _context.iot_datas.FromSql($"SELECT * FROM iot_datas WHERE sensing_dt BETWEEN {startDate} AND {endDate}").ToListAsync();

            // 3. resultType이 xml과 json에 따라 리턴하는 데이터형을 변경
            if (resultType == "xml") 
            { 
                // xml로 형변환
            }
            else if (resultType == "json")
            {
                // JSON으로 형변환
            }
            return result; // _context.iot_datas.ToListAsync();
        }

        // GET: api/IoT_Datas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<iot_datas>> GetIoT_Datas(int id)
        {
            var ioT_Datas = await _context.iot_datas.FindAsync(id);

            if (ioT_Datas == null)
            {
                return NotFound();
            }

            return ioT_Datas;
        }

        //// PUT: api/IoT_Datas/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutIoT_Datas(int id, iot_datas ioT_Datas)
        //{
        //    if (id != ioT_Datas.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(ioT_Datas).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!IoT_DatasExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/IoT_Datas
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<iot_datas>> PostIoT_Datas(iot_datas ioT_Datas)
        //{
        //    _context.iot_datas.Add(ioT_Datas);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetIoT_Datas", new { id = ioT_Datas.Id }, ioT_Datas);
        //}

        //// DELETE: api/IoT_Datas/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteIoT_Datas(int id)
        //{
        //    var ioT_Datas = await _context.iot_datas.FindAsync(id);
        //    if (ioT_Datas == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.iot_datas.Remove(ioT_Datas);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool IoT_DatasExists(int id)
        //{
        //    return _context.iot_datas.Any(e => e.Id == id);
        //}
    }
}
