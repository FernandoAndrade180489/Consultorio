using Consultorio.Context;
using Consultorio.Models.Entities;
using Consultorio.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consultorio.Repository
{
    public class ConsultaRepository : BaseRepository, IConsultaRepository
    {
        private readonly ConsultorioContext _context;

        public ConsultaRepository(ConsultorioContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Consulta>> GetConsultas()
        {
            return await _context.Consultas
                .Include(x => x.Paciente)
                .Include(x => x.Profissional)
                .Include(x => x.Especialidade)
                .ToListAsync();
        }

        public async Task<Consulta> GetConsultaById(int id)
        {

            return await _context.Consultas
            .Include(x => x.Paciente)
            .Include(x => x.Profissional)
            .Include(x => x.Especialidade)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
        }

        
    }
}
