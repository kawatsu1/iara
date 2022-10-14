using AutoMapper;
using Bogus;
using Bogus.DataSets;
using FluentAssertions;
using Iara.Domain.Entities;
using Iara.Infra.Repositories.Interfaces;
using Iara.Services.DTOS;
using Iara.Services.Services;
using Iara.Services.Services.Interfaces;
using Iara.Testes.Config;
using Iara.Testes.Fixtures;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace Iara.Testes
{
    public class CotacaoServiceTests
    {
        private readonly ICotacaoService _sut;
        private readonly IMapper _mapper;
        private readonly Mock<ICotacaoRepository> _cotacaoRepositoryMock;
        private readonly Mock<ICotacaoItemRepository> _cotacaoItemRepositoryMock;

        public CotacaoServiceTests()
        {
            _mapper = AutoMapperConfiguration.GetConfiguration();
            _cotacaoRepositoryMock = new Mock<ICotacaoRepository>();
            _cotacaoItemRepositoryMock = new Mock<ICotacaoItemRepository>();

            _sut = new CotacaoService(
                mapper: _mapper,
                cotacaoRepository: _cotacaoRepositoryMock.Object,
                cotacaoItemRepository: _cotacaoItemRepositoryMock.Object);
        }

        #region Create

        [Fact(DisplayName = "Create Valid Cotacao")]
        [Trait("Category", "Services")]
        public async Task Create_WhenCotacaoIsValid_ReturnsCotacaoDto()
        {
            // Arrange
            var cotacaoToCreate = CotacaoFixture.CreateValidCotacaoDTO();
            var cotacaoCreated = _mapper.Map<Cotacao>(cotacaoToCreate);

            _cotacaoRepositoryMock.Setup(x => x.GetAsync(
                It.IsAny<Expression<Func<Cotacao, bool>>>(),
                It.IsAny<bool>()))
            .ReturnsAsync(() => null);

            _cotacaoRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Cotacao>()))
                .ReturnsAsync(() => cotacaoCreated);

            // Act
            var result = await _sut.CreateAsync(cotacaoToCreate);

            // Assert
            result.Should()
                .BeEquivalentTo(_mapper.Map<CotacaoDto>(cotacaoCreated));
        }

        [Fact(DisplayName = "Create When Cotacao Exists")]
        [Trait("Category", "Services")]
        public async Task Create_WhenCotacaoExists_ReturnsNull()
        {
            // Arrange
            var cotacaoToCreate = CotacaoFixture.CreateValidCotacaoDTO();
            var cotacaoExists = CotacaoFixture.CreateValidCotacao();

            _cotacaoRepositoryMock.Setup(x => x.GetAsync(
                It.IsAny<Expression<Func<Cotacao, bool>>>(),
                It.IsAny<bool>()))
            .ReturnsAsync(() => cotacaoExists);

            // Act
            var result = await _sut.CreateAsync(cotacaoToCreate);


            // Act
            result.Should().BeNull();
        }

        [Fact(DisplayName = "Create When Cotacao is Invalid")]
        [Trait("Category", "Services")]
        public async Task Create_WhenCotacaoIsInvalid_ReturnsEmptyOptional()
        {
            // Arrange
            var cotacaoToCreate = CotacaoFixture.CreateInvalidCotacaoDTO();

            _cotacaoRepositoryMock.Setup(x => x.GetAsync(
                It.IsAny<Expression<Func<Cotacao, bool>>>(),
                It.IsAny<bool>()))
            .ReturnsAsync(() => null);

            // Act
            var result = await _sut.CreateAsync(cotacaoToCreate);


            // Act
            result.Should()
                .BeNull();
        }

        #endregion

        #region Update

        [Fact(DisplayName = "Update Valid Cotacao")]
        [Trait("Category", "Services")]
        public async Task Update_WhenCotacaoIsValid_ReturnsCotacaoDto()
        {
            // Arrange
            var oldCotacao = CotacaoFixture.CreateValidCotacao();
            var cotacaoToUpdate = CotacaoFixture.CreateValidCotacaoDTO();
            var cotacaoUpdated = _mapper.Map<Cotacao>(cotacaoToUpdate);

            var encryptedPassword = new Lorem().Sentence();

            _cotacaoRepositoryMock.Setup(x => x.GetAsnyc(oldCotacao.Id))
            .ReturnsAsync(() => oldCotacao);

            _cotacaoRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Cotacao>()))
                .ReturnsAsync(() => cotacaoUpdated);

            // Act
            var result = await _sut.UpdateAsync(cotacaoToUpdate);

            // Assert
            result.Should()
                .BeEquivalentTo(_mapper.Map<CotacaoDto>(cotacaoUpdated));
        }

        [Fact(DisplayName = "Update When Cotacao Not Exists")]
        [Trait("Category", "Services")]
        public async Task Update_WhenCotacaoNotExists_ReturnsEmptyOptional()
        {
            // Arrange
            var cotacaoToUpdate = CotacaoFixture.CreateValidCotacaoDTO();

            _cotacaoRepositoryMock.Setup(x => x.GetAsync(
                It.IsAny<Expression<Func<Cotacao, bool>>>(),
                It.IsAny<bool>()))
            .ReturnsAsync(() => null);

            // Act
            var result = await _sut.UpdateAsync(cotacaoToUpdate);

            // Act
            result.Should()
                .BeNull();
        }

        [Fact(DisplayName = "Update When Cotacao is Invalid")]
        [Trait("Category", "Services")]
        public async Task Update_WhenCotacaoIsInvalid_ReturnsEmptyOptional()
        {
            // Arrange
            var oldCotacao = CotacaoFixture.CreateValidCotacao();
            var cotacaoToUpdate = CotacaoFixture.CreateInvalidCotacaoDTO();

            _cotacaoRepositoryMock.Setup(x => x.GetAsync(
                It.IsAny<Expression<Func<Cotacao, bool>>>(),
                It.IsAny<bool>()))
            .ReturnsAsync(() => oldCotacao);

            // Act
            var result = await _sut.UpdateAsync(cotacaoToUpdate);


            // Act
            result.Should()
                .BeNull();
        }

        #endregion

        #region Remove

        [Fact(DisplayName = "Remove Cotacao")]
        [Trait("Category", "Services")]
        public async Task Remove_WhenCotacaoExists_RemoveCotacao()
        {
            // Arrange
            var cotacaoId = new Randomizer().Int(0, 1000);

            _cotacaoRepositoryMock.Setup(x => x.RemoveAsync(It.IsAny<int>()))
                .Verifiable();

            // Act
            await _sut.RemoveAsync(cotacaoId);

            // Assert
            _cotacaoRepositoryMock.Verify(x => x.RemoveAsync(cotacaoId), Times.Once);
        }

        #endregion

        #region Get

        [Fact(DisplayName = "Get By Id")]
        [Trait("Category", "Services")]
        public async Task GetById_WhenCotacaoExists_ReturnsCotacaoDto()
        {
            // Arrange
            var cotacaoId = new Randomizer().Int(0, 1000);
            var cotacaoFound = CotacaoFixture.CreateValidCotacao();

            _cotacaoRepositoryMock.Setup(x => x.GetAsnyc(cotacaoId))
            .ReturnsAsync(() => cotacaoFound);

            // Act
            var result = await _sut.GetAsync(cotacaoId);

            // Assert
            result.Should()
                .BeEquivalentTo(_mapper.Map<CotacaoDto>(cotacaoFound));
        }

        [Fact(DisplayName = "Get By Id When Cotacao Not Exists")]
        [Trait("Category", "Services")]
        public async Task GetById_WhenCotacaoNotExists_ReturnsEmptyOptional()
        {
            // Arrange
            var cotacaoId = new Randomizer().Int(0, 1000);

            _cotacaoRepositoryMock.Setup(x => x.GetAsnyc(cotacaoId))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetAsync(cotacaoId);

            // Assert
            result.Should()
                .BeNull();
        }


        [Fact(DisplayName = "Get All")]
        [Trait("Category", "Services")]
        public async Task GetAllCotacaos_WhenCotacaosExists_ReturnsAListOfCotacaoDto()
        {
            // Arrange
            var cotacaosFound = CotacaoFixture.CreateListValidCotacao();

            _cotacaoRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(() => cotacaosFound);

            // Act
            var result = await _sut.GetAllAsync();

            // Assert
            result.Should()
                .BeEquivalentTo(_mapper.Map<List<CotacaoDto>>(cotacaosFound));
        }

        [Fact(DisplayName = "Get All Cotacaos When None Cotacao Found")]
        [Trait("Category", "Services")]
        public async Task GetAllCotacaos_WhenNoneCotacaoFound_ReturnsEmptyList()
        {
            // Arrange

            _cotacaoRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetAllAsync();

            // Assert
            result.Should()
                .BeEmpty();
        }

        #endregion
    }
}
