using dario_simic_iis_web_api.Model;
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
using Microsoft.OpenApi.Models;
using SoapCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
            Appointments.Add(new DentistAppointment(new Dentist("TOG"), new Patient("TEST")));
            Appointments.Add(new DentistAppointment(new Dentist("TOG"), new Patient("TEST")));
            Appointments.Add(new DentistAppointment(new Dentist("TOG"), new Patient("TEST")));
            Appointments.Add(new DentistAppointment(new Dentist("TOG"), new Patient("TEST")));
            Appointments.Add(new DentistAppointment(new Dentist("TOG"), new Patient("TEST")));
            Appointments.Add(new DentistAppointment(new Dentist("TOG"), new Patient("TEST")));
            Appointments.Add(new DentistAppointment(new Dentist("TOG"), new Patient("TEST")));
            Appointments.Add(new DentistAppointment(new Dentist("TOG"), new Patient("TEST")));
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
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
