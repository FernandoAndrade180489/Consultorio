﻿using Consultorio.Context;
using Consultorio.Models.Entities;
using Consultorio.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consultorio.Repository
{
    public class PacienteRepository : BaseRepository, IPacienteRepository
    {
        private readonly ConsultorioContext _context;

        public PacienteRepository(ConsultorioContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Paciente>> GetPacientesAsync()
        {
            // Include Consultas para trazer as consultas viculadas ao paciente usando EntityFramework
            return await _context.Pacientes
                .Include(x => x.Consultas).ToListAsync();
        }

        public async Task<Paciente> GetPacientesByIdAsync(int id)
        {
            return await _context.Pacientes
                .Where(x => x.Id == id).Include(x => x.Consultas).FirstOrDefaultAsync();
        }
    }
}