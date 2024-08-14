namespace Tgs.GameBets.RapidTest.Worker.Core;
public record GameBetRapidTestWorkerSettingsOptions
{
	public int ChannelExpiry { get; set; } = 900;
}
