﻿namespace PushThenPause.Business.Dtos;

public class DashboardDto
{
    public string DisplayName { get; set; } = string.Empty;
    public int StreakCount { get; set; }
    public bool NemsModEnabled { get; set; }
    public List<CycleDto> CycleDtos { get; set; } = new List<CycleDto>();
}
