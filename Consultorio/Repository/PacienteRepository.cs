using Consultorio.Context;
using Consultorio.Models.Entities;
using Consultorio.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Consultorio.Repository
{
    public class PacienteRepository : BaseRepository, IPacienteRepository
    {
        private readonly ConsultorioContext _context;

        public PacienteRepository(ConsultorioContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Paciente> GetPacientes()
        {
            // Include Consultas para trazer as consultas viculadas ao paciente usando EntityFramework
            return _context.Pacientes.Include(x => x.Consultas).ToList();
        }

        public Paciente GetPacientesById(int id)
        {
            return _context.Pacientes.Where(x => x.Id == id).Include(x => x.Consultas).FirstOrDefault();
        }
    }
}
