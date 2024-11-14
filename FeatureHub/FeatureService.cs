﻿using System;
using FeatureHubSDK;
using IO.FeatureHub.SSE.Model;


namespace FeatureHub;

public class FeatureService
{
    private bool _isFeatureDkEnabled;
    private bool _isFeatureMeasurementEnabled;
    private bool _isFeaturePatientEnabled;

    public FeatureService()
    {
        FeatureLogging.DebugLogger += (sender, s) => Console.WriteLine("DEBUG: " + s);
        FeatureLogging.TraceLogger += (sender, s) => Console.WriteLine("TRACE: " + s);
        FeatureLogging.InfoLogger += (sender, s) => Console.WriteLine("INFO: " + s);
        FeatureLogging.ErrorLogger += (sender, s) => Console.WriteLine("ERROR: " + s);
    }

    public async Task Connect()
    {
        var config = new EdgeFeatureHubConfig("http://featurehub:8085", "58cc5eef-6bd0-438d-980d-fefb04043681/3s3TJTdF0RORXds6nWcL957z6JYaWBgQn6780UNs");
        var fh = await config.NewContext().Country(StrategyAttributeCountryName.Denmark).Build();
        _isFeatureMeasurementEnabled = fh["m_service"].IsEnabled;
        _isFeaturePatientEnabled = fh["p_service"].IsEnabled;
        _isFeatureDkEnabled = fh["o_dk"].IsEnabled;
    }

    public virtual bool FeatureFlagChecker(Features feature)
    {
        Console.WriteLine("FeatureFlagChecker");
        Console.WriteLine(_isFeatureDkEnabled);
        Console.WriteLine(_isFeatureMeasurementEnabled);
        Console.WriteLine(_isFeaturePatientEnabled);
        switch (feature)
        {
            case Features.MeasurementService:
                if (_isFeatureDkEnabled && _isFeatureMeasurementEnabled)
                {
                    return true;
                }
                return false;
                break;
            case Features.PatientService:
                if (_isFeatureDkEnabled && _isFeaturePatientEnabled)
                {
                    return true;
                }
                return false;
                break;
            default:
                return false;
                break;
        }
        return false;
    }
}
