using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstractions.Interfaces;
using Core.Models;
using DAL.Abstractions.Interfaces;
using DAL.Services;

namespace BLL.Services
{
    public class BookingService : GenericService<Booking>, IBookingService
    {
        private readonly IClassService _classService;
        private readonly IMemberService _memberService;

        public BookingService(IRepository<Booking> repository, IClassService classService, IMemberService memberService)
            : base(repository)
        {
            _classService = classService;
            _memberService = memberService;
        }

        public async Task<Booking> BookClass(Guid memberId, Guid classId)
        {
            var user = await GetById(memberId);
            var clas = await _classService.GetById(classId);
            user.Class = clas;
            return user;
        }

        public async Task<List<Booking>> GetBookingsByMember(Guid memberId)
        {
            var user = await GetById(memberId);
            
            var bookings = await GetAll();
            var bookingByMember = bookings.Where(i => i.Class == user.Class).ToList();
            if (bookingByMember == null)
            {
                throw new Exception("User not found");
            }
            return bookingByMember;
        }

        public async Task<List<Booking>> GetBookingsByClass(Guid classId)
        {
            var fitnesClass = await GetById(classId);
            var bookings = await GetAll();
            var bookingByClass = bookings.Where(i => i.Class == fitnesClass.Class).ToList();
            if (bookingByClass == null)
            {
                throw new Exception("Booking not found");
            }
            return bookingByClass;
        }

        public async Task<List<Booking>> GetBookingsByDate(DateTime date)
        {
            var bookings = await GetAll();
            var bookingByDate = bookings.Where(i => i.Date == date).ToList();
            if (bookingByDate == null)
            {
                throw new Exception("User not found");
            }
            return bookingByDate;
        }

        public async Task ConfirmBooking(Guid bookingId)
        {
            var booking = await GetById(bookingId);
            booking.IsConfirmed = true;
        }
    }
}