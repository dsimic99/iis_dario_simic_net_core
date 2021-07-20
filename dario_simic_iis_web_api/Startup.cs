using dario_simic_iis_web_api.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SoapCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace dario_simic_iis_web_api
{
    

    public class Startup
    {
        public static List<DentistAppointment> Appointments;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Appointments = new List<DentistAppointment>();
            Appointments.Add(new DentistAppointment(new Dentist("Dr. Darko Mendes"), new Patient("Luka Simic")));
            Appointments.Add(new DentistAppointment(new Dentist("Dr. Ivan Tog"), new Patient("Dario Simic")));
            Appointments.Add(new DentistAppointment(new Dentist("Celo Mepas"), new Patient("Ivan Truljo")));
            Appointments.Add(new DentistAppointment(new Dentist("Zarog Bevanda"), new Patient("Antisa Burko")));
            Appointments.Add(new DentistAppointment(new Dentist("Dr. Stjepan Ivic"), new Patient("Luka Modric")));
            Appointments.Add(new DentistAppointment(new Dentist("Mario Mense"), new Patient("Florip Popic")));
            Appointments.Add(new DentistAppointment(new Dentist("Mr. Florian"), new Patient("Kosonja Pravi")));
            Appointments.Add(new DentistAppointment(new Dentist("Cevap Kevap"), new Patient("Stjepan Ibicevic")));
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Configuration["Jwt:Issuer"],
            ValidAudience = Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
        };
    });

            services.TryAddSingleton<IAppointmentService, AppointmentService>();
            services.AddMvc();

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.AddControllers().AddXmlDataContractSerializerFormatters();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSoapEndpoint<IAppointmentService>("/AppointmentService.asmx", new BasicHttpBinding(), SoapSerializer.XmlSerializer);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
