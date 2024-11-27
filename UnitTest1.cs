using ValuteConverter.Model.Interfaces;

namespace Test2
{
    public class CoinYepParserTests
    {
        [TestClass]
        public class ViewModelTests
        {
            private ViewModel _viewModel;
            private Mock<ICourseConverter> _mockConverter;

            [TestInitialize]
            public void Initialize()
            {
                _mockConverter = new Mock<ICourseConverter>();
                _viewModel = new ViewModel(_mockConverter.Object);
            }

            [TestMethod]
            public async Task LoadValutes_Should_Load_Valutes_From_Converter()
            {
                // Arrange
                var mockValuteEntries = new List<ValuteEntry>
            {
                new ValuteEntry { Code = "USD", Name = "US Dollar", ValuteCourse = 1.0 },
                new ValuteEntry { Code = "EUR", Name = "Euro", ValuteCourse = 0.85 }
            };
                _mockConverter.Setup(c => c.GetExchangeRateOnDateAsync(It.IsAny<DateTime>())).ReturnsAsync(mockValuteEntries);

                // Act
                await _viewModel.LoadValutes();

                // Assert
                Assert.AreEqual(mockValuteEntries.Count, _viewModel.ValuteEntries.Count);
                Assert.IsTrue(mockValuteEntries.SequenceEqual(_viewModel.ValuteEntries));
            }

            [TestMethod]
            public void ChangeSides_Should_Swap_SelectedValutes_And_Convert()
            {
                // Arrange
                _viewModel.ValuteEntries = new System.Collections.ObjectModel.ObservableCollection<ValuteEntry>
            {
                new ValuteEntry { Code = "USD", Name = "US Dollar", ValuteCourse = 1.0 },
                new ValuteEntry { Code = "EUR", Name = "Euro", ValuteCourse = 0.85 }
            };
                _viewModel.SelectedValuteLeft = _viewModel.ValuteEntries[0];
                _viewModel.SelectedValuteRight = _viewModel.ValuteEntries[1];

                // Act
                _viewModel.ChangeSides.Execute(null);

                // Assert
                Assert.AreEqual(_viewModel.ValuteEntries[1], _viewModel.SelectedValuteLeft);
                Assert.AreEqual(_viewModel.ValuteEntries[0], _viewModel.SelectedValuteRight);
            }
        }
    }