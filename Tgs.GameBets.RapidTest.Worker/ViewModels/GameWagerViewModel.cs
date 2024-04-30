using System.ComponentModel.DataAnnotations;
using Tgs.GameRecord.Handle.Common.Protos;

namespace Tgs.GameBets.RapidTest.Worker.ViewModels;

public record GameWagerViewModel
{
	/// <summary>
	/// The game provider identifier
	/// </summary>
	[Required]
	public Guid GameProviderId { get; init; }
	/// <summary>
	/// The game provider code
	/// </summary>
	[Required]
	public string GameProviderCode { get; init; } = default!;
	/// <summary>
	/// The game provider configuration identifier
	/// </summary>
	[Required]
	public Guid GameProviderConfigurationId { get; init; }
	/// <summary>
	/// The sub game provider code
	/// </summary>
	[Required]
	public string SubGameProviderCode { get; init; } = default!;
	/// <summary>
	/// The game identifier
	/// </summary>
	[Required]
	public int GameId { get; init; }
	/// <summary>
	/// The game category
	/// </summary>
	[Required]
	public GameCategories GameCategory { get; init; }
	/// <summary>
	/// The game type
	/// </summary>
	[Required]
	public GameTypes GameType { get; init; }
	/// <summary>
	/// The reseller identifier
	/// </summary>
	[Required]
	public Guid ResellerId { get; init; }
	/// <summary>
	/// The reseller code
	/// </summary>
	[Required]
	public string ResellerCode { get; init; } = default!;
	/// <summary>
	/// The licensee identifier
	/// </summary>
	[Required]
	public Guid LicenseeId { get; init; }
	/// <summary>
	/// The licensee code
	/// </summary>
	[Required]
	public string LicenseeCode { get; init; } = default!;
	/// <summary>
	/// The brand identifier
	/// </summary>
	[Required]
	public Guid BrandId { get; init; }
	/// <summary>
	/// The brand code
	/// </summary>
	[Required]
	public string BrandCode { get; init; } = default!;
	/// <summary>
	/// The player code/
	/// </summary>
	[Required]
	public string PlayerCode { get; init; } = default!;
	/// <summary>
	/// The username
	/// </summary>
	[Required]
	public string Username { get; init; } = default!;
	/// <summary>
	/// The currency code
	/// </summary>
	[Required]
	public string CurrencyCode { get; init; } = default!;
	/// <summary>
	/// The wallet identifier
	/// </summary>
	[Required]
	public Guid WalletId { get; init; }
	/// <summary>
	/// The wager state
	/// </summary>
	[Required]
	public WagerStates WagerState { get; init; }
	/// <summary>
	/// The wager type
	/// </summary>
	[Required]
	public WagerTypes WagerType { get; init; }
	/// <summary>
	/// The closed state
	/// </summary>
	[Required]
	public ClosedStates ClosedState { get; init; }
	/// <summary>
	/// The void states
	/// </summary>
	[Required]
	public VoidStates VoidState { get; init; }
	/// <summary>
	/// The cancel state
	/// </summary>
	[Required]
	public CancelStates CancelState { get; init; }
	/// <summary>
	/// The free game state
	/// </summary>
	[Required]
	public FreeGameStates FreeGameState { get; set; }
	/// <summary>
	/// The gamble state
	/// </summary>
	[Required]
	public GambleStates GambleState { get; set; }
	/// <summary>
	/// The cashout state
	/// </summary>
	[Required]
	public CashoutStates CashoutState { get; set; }
	/// <summary>
	/// The resettle state
	/// </summary>
	[Required]
	public ResettleStates ResettleState { get; set; }
	/// <summary>
	/// The external round identifier
	/// </summary>
	[Required]
	public string ExternalRoundId { get; init; } = default!;
	/// <summary>
	/// The external wager identifier
	/// </summary>
	[Required]
	public string ExternalWagerId { get; init; } = default!;
	/// <summary>
	/// The external game identifier
	/// </summary>
	[Required]
	public string ExternalGameId { get; init; } = default!;
	/// <summary>
	/// The external created on
	/// </summary>
	[Required]
	public DateTimeOffset ExternalCreatedOn { get; init; }
	/// <summary>
	/// The external closed on
	/// </summary>
	[Required]
	public DateTimeOffset ExternalClosedOn { get; init; }
	/// <summary>
	/// The external payoff on
	/// </summary>
	[Required]
	public DateTimeOffset ExternalPayoffOn { get; init; }
	/// <summary>
	/// The external resettle on
	/// </summary>
	[Required]
	public DateTimeOffset ExternalResettleOn { get; set; }
	/// <summary>
	/// The exchange rate
	/// </summary>
	[Required]
	public decimal CurrencyRatio { get; init; }
	/// <summary>
	/// The bet
	/// </summary>
	[Required]
	public decimal Bet { get; init; }
	/// <summary>
	/// The valid bet
	/// </summary>
	[Required]
	public decimal ValidBet { get; init; }
	/// <summary>
	/// The settle
	/// </summary>
	[Required]
	public decimal Settle { get; init; }
	/// <summary>
	/// The commission
	/// </summary>
	[Required]
	public decimal Commission { get; init; }
	/// <summary>
	/// The turnover
	/// </summary>
	[Required]
	public decimal Turnover { get; init; }
	/// <summary>
	/// The win loss
	/// </summary>
	[Required]
	public decimal WinLoss { get; init; }
	/// <summary>
	/// The additional data
	/// </summary>
	[Required]
	public string AdditionalData { get; init; } = default!;
}
