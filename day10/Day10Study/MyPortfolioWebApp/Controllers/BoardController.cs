﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPortfolioWebApp.Models;

namespace MyPortfolioWebApp.Controllers
{
    public class BoardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BoardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Board
        // param int page는 처음 게시판은 무조건 1페이지부터 시작
        public async Task<IActionResult> Index(int page = 1, string search = "")
        {
            // 뷰쪽에 보내고 싶은 데이터
            // ViewData["Key"] // ViewBag.Title // TempData["Key"]
            ViewData["Title"] = "서버에서 변경가능!!";

            // 최종단계
            // 페이지 개수
            //var totalCount = _context.Board.FromSql($@"select Id from Board where Title like '%{search}%'").Count(); // 전체 게시글 수 (현재 오류 발생 밑 줄 코드 사용!)
            var totalCount = _context.Board.Where(n => EF.Functions.Like(n.Title, $"%{search}%")).Count();
            var countList = 10; // 한 페이지에 기본 게시글 개수 10개
            var totalPage = totalCount / countList; // 한 페이지 당 개수로 나누면 전체페이지 수

            // HACK : 게시판 페이지 중요 로직. 남는 데이터도 한페이지를 차지해야 함
            if (totalCount % countList > 0) totalPage++;   // 남은 게시글이 있으면 페이지 수 증가
            if (totalPage < page) page = totalPage;

            // 마지막 페이지 구하기
            var countPage = 10; // 페이지를 표시할 최대페이지 개수, 10개
            var startPage = (page - 1) / countPage * countPage + 1;
            var endPage = startPage + countPage - 1;

            // HACK : 나타낼 페이지 수가 10이 안되면 페이지 수 조정 
            // 마지막 페이지까지 글이 12개면 1, 2 페이지만 표시
            if (totalPage < endPage) endPage = totalPage;

            // 저장 프로시저에 보낼 rowNum 값, 시작번호랑 끝 번호
            var startCount = (page - 1) * countPage + 1; // 2페이지의 경우 11
            var endCount = startCount + countList - 1;  // 2페이지의 경우 20

            // View로 넘기는 데이터, 페이징 숫자 컨트롤 사용
            ViewBag.StartPage = startPage;
            ViewBag.EndPage = endPage;
            ViewBag.Page = page;
            ViewBag.TotalPage = totalPage;
            ViewBag.Search = search;    // 검색어

            // 저장프로시저 호출
            var board = await _context.Board.FromSql($"CALL New_PagingBoard({startCount}, {endCount}, {search})").ToListAsync();
            return View(board);
        }

        // GET: Board/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var board = await _context.Board
                .FirstOrDefaultAsync(m => m.Id == id);
            if (board == null)
            {
                return NotFound();
            }

            // 조회수 증가 로직
            board.ReadCount++;
            _context.Board.Update(board);
            await _context.SaveChangesAsync();

            return View(board);
        }

        // GET: Board/Create
        public IActionResult Create()
        {
            var board = new Board
            {
                Writer = "관리자",
                PostDate = DateTime.Now,
                ReadCount = 0,
            };
            return View();  // View로 데이터를 가져갈게 아무것도 없음
        }

        // POST: Board/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Contents")] Board board)
        {
            if (ModelState.IsValid)
            {
                board.Writer = "관리자";    // 작성자는 자동으로 관리자
                board.PostDate = DateTime.Now;   // 게시일자는 현재
                board.ReadCount = 0;

                _context.Add(board);
                await _context.SaveChangesAsync();

                TempData["success"] = "게시글 작성 성공!";

                return RedirectToAction(nameof(Index));
            }
            return View(board);
        }

        // GET: Board/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var board = await _context.Board.FindAsync(id);
            if (board == null)
            {
                return NotFound();
            }
            return View(board);
        }

        // POST: Board/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Contents")] Board board)
        {
            if (id != board.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // 방식2 - 원본을 찾아서 수정해주는 방식
                    var existingBoard = await _context.Board.FindAsync(id);
                    if (existingBoard == null)
                    {
                        return NotFound();
                    }

                    existingBoard.Title = board.Title;
                    existingBoard.Contents = board.Contents;

                    // UPDATE Board SET ...
                    //_context.Update(board);    // 방식1 - ID가 같은 새글을 업데이트하면 수정
                    // COMMIT
                    await _context.SaveChangesAsync();
                    TempData["success"] = "게시판 수정 성공!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoardExists(board.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(board);
        }

        // GET: Board/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var board = await _context.Board
                .FirstOrDefaultAsync(m => m.Id == id);
            if (board == null)
            {
                return NotFound();
            }

            return View(board);
        }

        // POST: Board/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var board = await _context.Board.FindAsync(id);
            if (board != null)
            {
                _context.Board.Remove(board);
            }

            await _context.SaveChangesAsync();
            TempData["success"] = "게시글 삭제 성공!";
            return RedirectToAction(nameof(Index));
        }

        private bool BoardExists(int id)
        {
            return _context.Board.Any(e => e.Id == id);
        }
    }
}
