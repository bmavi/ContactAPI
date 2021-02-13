using System;
using Xunit;
using AutoMapper;
using Contact.Data;
using Contact.Models;
using Contact.Api.AutoMapperProfiles;
using Contact.Api.Controllers;
using Contact.Api.DTOs;
using Moq;
using System.Collections.Generic;
//using Contact.Tests.DataTest;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using System.Threading.Tasks;

namespace Contact.Tests
{
    public class ContactUnitTests
    {
        private readonly IEnumerable<ContactInfo> _contactInfo;
        private readonly Mock<IGenericRepository<ContactInfo>> _repository;
        private readonly IMapper _mapper;
        public ContactUnitTests()
        {           
            //configuring automapper
            var profiles = new AutoMapperProfiles();
            var configuration = new MapperConfiguration(config => config.AddProfile(profiles));
            _mapper = new Mapper(configuration);

            //intiatializing repository
            _repository = new Mock<IGenericRepository<ContactInfo>>();

            // get sample data
            _contactInfo = SampleData.getContactInfoData();
        }
       
        [Fact]
        public async Task Task_GetAllContacts_ReturningAllContact()
        {
            // arrange phase           
            _repository.Setup(t => t.GetAllAsync()).ReturnsAsync(_contactInfo);

            // processing phase
            var controller = new ContactController(_repository.Object, _mapper);             
            var result = await controller.GetAllAsync();             

            // assert phase
            Assert.NotNull(result);
            var actionResult = Assert.IsType<ActionResult<IEnumerable<ContactInfo>>>(result);
            var objResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualContacts = objResult.Value;
            IEnumerable<ContactInfo> actualResult = _mapper.Map<IEnumerable<ContactInfo>>(actualContacts);
            actualResult.Should().BeEquivalentTo(_contactInfo, t => t.ComparingByMembers<ContactInfo>());

        }
        
        [Fact]
        public async Task Task_AddNewContactAsync_AddNewContact_Valid_Data_ReturningSuccessMessage()
        {
            // arrange phase  
            var contactInfo = _contactInfo.FirstOrDefault();
            ContactInfoDTO contactInfoDTO = _mapper.Map<ContactInfoDTO>(contactInfo);

            _repository.Setup(t => t.AddTAsync(contactInfo)).ReturnsAsync(1);

            // mock returing created record
            _repository.Setup(t => t.GetOneByAsync(t=>t.Id == contactInfo.Id)).ReturnsAsync(contactInfo);

            // processing phase
            var controller = new ContactController(_repository.Object, _mapper);

            var result = await controller.AddNewContactAsync(contactInfo);

            // assert phase
       
            var objResult = Assert.IsType<CreatedResult>(result.Result);
            var statusResult = objResult.StatusCode;
            var actualResult = objResult.Value as ContactInfoDTO;

            //assert result
            Assert.NotNull(result);

            //assert statusCode
            Assert.Equal(201, statusResult);

            //assert validate        
            actualResult.Should().BeEquivalentTo(contactInfoDTO, t => t.ComparingByMembers<ContactInfoDTO>());

        }
        [Fact]
        public async Task Task_AddNewContactAsync_AddNewContact_Null_Data_ReturningFailureMessage()
        {
            // arrange phase  
            ContactInfo contactInfo = new ContactInfo();           

            _repository.Setup(t => t.AddTAsync(null)).ReturnsAsync(0);

            // processing phase
            var controller = new ContactController(_repository.Object, _mapper);

            var result = await controller.AddNewContactAsync(contactInfo);

            // assert phase
            var objResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var statusResult = objResult.StatusCode;
            var actualResult = objResult.Value;

            //assert result
            Assert.NotNull(result);

            //assert statusCode
            Assert.Equal(404, statusResult);

            //assert validate        
            Assert.Equal("No New Contact created", actualResult);

        }
        [Fact]
        public async Task Task_UpdateContactAsync_UpdateContact_Valid_Data_ReturningSuccessMessage()
        {
            // arrange phase  
            var contactInfo = _contactInfo.FirstOrDefault();
            ContactInfoDTO contactInfoDTO = _mapper.Map<ContactInfoDTO>(contactInfo);
            
            // mock returing record to update
            _repository.Setup(t => t.GetOneByAsync(t => t.Id == contactInfo.Id)).ReturnsAsync(contactInfo);

            _repository.Setup(t => t.UpdateTAsync(contactInfo)).ReturnsAsync(1);

            // processing phase
            var controller = new ContactController(_repository.Object, _mapper);


            var result = await controller.UpdateContactAsync(contactInfo.Id,contactInfo);

            // assert phase

            var objResult = Assert.IsType<OkObjectResult>(result.Result);
            var statusResult = objResult.StatusCode;
            var actualResult = objResult.Value as ContactInfoDTO;

            //assert result
            Assert.NotNull(result);

            //assert statusCode
            Assert.Equal(200, statusResult);

            //assert validate        
            actualResult.Should().BeEquivalentTo(contactInfoDTO, t => t.ComparingByMembers<ContactInfoDTO>());
        }
        [Fact]
        public async Task Task_UpdateContactAsync_UpdateContact_Null_Data_ReturningFailureMessage()
        {
            // arrange phase  
            ContactInfo contactInfo = new ContactInfo();          

            _repository.Setup(t => t.UpdateTAsync(contactInfo)).ReturnsAsync(0);

            // processing phase
            var controller = new ContactController(_repository.Object, _mapper);

            var result = await controller.UpdateContactAsync(Guid.NewGuid().ToString(),contactInfo);

            // assert phase
            var objResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var statusResult = objResult.StatusCode;
            var actualResult = (string)objResult.Value;

            //assert result
            Assert.NotNull(result);

            //assert statusCode
            Assert.Equal(404, statusResult);

            //assert validate        
            Assert.Equal("No Contact Updated", actualResult);

        }

        [Theory]
        [InlineData("2865578b-32da-4a8c-8db9-76f41be167d1")]
        public async Task Task_DeleteContactAsync_DeletingContact_ReturningSuccessMessage(string id)
        {
            // arrange phase  
            var contactInfo = _contactInfo.FirstOrDefault();
            _repository.Setup(t => t.GetOneByAsync(t=>t.Id == id)).ReturnsAsync(contactInfo);
            // mock returing delete action
            _repository.Setup(t => t.DeleteTAsync(contactInfo)).ReturnsAsync(1);

            // processing phase
            var controller = new ContactController(_repository.Object, _mapper);
            var result = await controller.DeleteContactAsync(id);

            // assert phase
            var objResult = Assert.IsType<OkObjectResult>(result.Result);
            var statusResult = objResult.StatusCode;
            var actualResult = (int)objResult.Value;

            //assert result
            Assert.NotNull(result);

            //assert statusCode
            Assert.Equal(200, statusResult);

            //assert validate
            Assert.Equal(1,actualResult);

        }


        [Theory]
        [InlineData("2865578b-32da-4a8c-8db9-76f41be167d8")]
        public async Task Task_DeleteContactAsync_DeletingContact_ReturningFailureMessage(string id)
        {
            // arrange phase  

            //set null value 
            ContactInfo contactInfo = null; //Return null value 
            _repository.Setup(t => t.GetOneByAsync(t => t.Id == id)).ReturnsAsync(contactInfo);
            // mock returing delete action with zero result
            _repository.Setup(t => t.DeleteTAsync(contactInfo)).ReturnsAsync(0);

            // processing phase
            var controller = new ContactController(_repository.Object, _mapper);
            var result = await controller.DeleteContactAsync(id);

            // assert phase
            var objResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var statusResult = objResult.StatusCode;
            var actualResult = (string)objResult.Value;

            //assert result
            Assert.NotNull(result);

            //assert statusCode
            Assert.Equal(404, statusResult);

            //assert validate
            Assert.Equal("No Contact Data", actualResult);

        }


    }
    }
