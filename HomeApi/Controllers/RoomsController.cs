﻿using AutoMapper;
using HomeApi.Contracts.Models.Rooms;
using HomeApi.Data.Models;
using HomeApi.Data.Queries;
using HomeApi.Data.Repos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HomeApi.Controllers
{
    /// <summary>
    /// Контроллер комнат
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        private IRoomRepository _repository;
        private IMapper _mapper;

        public RoomsController(IRoomRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }



        /// <summary>
        /// Добавление комнаты
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Add([FromBody] AddRoomRequest request)
        {
            var existingRoom = await _repository.GetRoomByName(request.Name);
            if (existingRoom == null)
            {
                var newRoom = _mapper.Map<AddRoomRequest, Room>(request);
                await _repository.AddRoom(newRoom);
                return StatusCode(201, $"Комната {request.Name} добавлена!");
            }

            return StatusCode(409, $"Ошибка: Комната {request.Name} уже существует.");
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> EditRoom([FromRoute] Guid id,
            [FromBody] EditRoomRequest request)
        {

            var room = await _repository.GetRoomId(id);
            if (room == null)
            {
                return StatusCode(400, $" Комнаты с номером ID: {id} не существует!");
            }

            await _repository.UpdateRoom(
               room,
               new UpdateRoomQuery(request.NewName)
           );

            return StatusCode(200, $"Название комнаты изменено на новое {room.Name}");

        }
    }
}