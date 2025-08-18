using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetInformationAreaValues : MonoBehaviour
{
    [Header("Spacebody")]
    [SerializeField] private TextMeshProUGUI nameValueTextB;
    [SerializeField] private TextMeshProUGUI discoveryDateValueTextB;
    [SerializeField] private TextMeshProUGUI discovererValueTextB;
    [SerializeField] private TextMeshProUGUI ageDateValueTextB;
    [SerializeField] private TextMeshProUGUI parentValueTextB;
    [SerializeField] private TextMeshProUGUI typeValueTextB;
    [SerializeField] private TextMeshProUGUI mainInfoValueTextB;
    [SerializeField] private TextMeshProUGUI subInfoValueTextB;
    [SerializeField] private TextMeshProUGUI massInfoValueTextB;
    [SerializeField] private TextMeshProUGUI luminosityInfoValueTextB;
    [SerializeField] private TextMeshProUGUI diameterInfoValueTextB;
    [SerializeField] private TextMeshProUGUI velocityInfoValueTextB;
    [SerializeField] private TextMeshProUGUI temperatureInfoValueTextB;
    [SerializeField] private TextMeshProUGUI distanceFromParentInfoValueTextB;
    [SerializeField] private TextMeshProUGUI rotationAngleInfoValueTextB;
    [SerializeField] private TextMeshProUGUI rotationSpeedInfoValueTextB;
    [SerializeField] private TextMeshProUGUI atmosphereThicknessInfoValueTextB;
    [SerializeField] private TextMeshProUGUI mainColorInfoValueTextB;
    [SerializeField] private TextMeshProUGUI subColorInfoValueTextB;
    [SerializeField] private TextMeshProUGUI ringDistanceInfoValueTextB;
    [SerializeField] private TextMeshProUGUI ringSizeInfoValueTextB;
    [SerializeField] private TextMeshProUGUI ringMainColorInfoValueTextB;
    [SerializeField] private TextMeshProUGUI ringSubColorInfoValueTextB;
    [SerializeField] private TextMeshProUGUI ringDetailColorInfoValueTextB;

    [Header("Astronomer")]
    [SerializeField] private TextMeshProUGUI nameInfoValueTextA;
    [SerializeField] private TextMeshProUGUI dateOfBirthInfoValueTextA;
    [SerializeField] private TextMeshProUGUI birthPlaceInfoValueTextA;
    [SerializeField] private TextMeshProUGUI occupationInfoValueTextA;
    [SerializeField] private TextMeshProUGUI descriptionInfoValueTextA;
    [SerializeField] private TextMeshProUGUI marriedInfoValueTextA;
    [SerializeField] private TextMeshProUGUI genderInfoValueTextA;
    [SerializeField] private TextMeshProUGUI telescopeAmountInfoValueTextA;

    public void SetSpaceBodyValues(SpaceBody spaceBody)
    {
        nameValueTextB.text = spaceBody.Name;
        discoveryDateValueTextB.text = spaceBody.DiscoveryDate.Date.ToString();
        //TODO discovererValueTextB.text = spaceBody.DiscovererId.ToString();
        ageDateValueTextB.text = spaceBody.Age.ToString();
        //TODO parentValueTextB.text = spaceBody.ParentId.ToString();
        typeValueTextB.text = spaceBody.Type.ToString();
        mainInfoValueTextB.text = spaceBody.MainInfo;
        subInfoValueTextB.text = spaceBody.SubInfo;
        massInfoValueTextB.text = spaceBody.Mass.ToString();
        luminosityInfoValueTextB.text = spaceBody.Luminosity.ToString();
        diameterInfoValueTextB.text = spaceBody.Diameter.ToString();
        velocityInfoValueTextB.text = spaceBody.Velocity.ToString();
        temperatureInfoValueTextB.text = spaceBody.Temperature.ToString();
        distanceFromParentInfoValueTextB.text = spaceBody.DistanceFromParent.ToString();
        rotationAngleInfoValueTextB.text = spaceBody.RotationAngle.ToString();
        rotationSpeedInfoValueTextB.text = spaceBody.RotationSpeed.ToString();
        atmosphereThicknessInfoValueTextB.text = spaceBody.AtmosphereThickness.ToString();
        mainColorInfoValueTextB.text = spaceBody.MainColorHex;
        subColorInfoValueTextB.text = spaceBody.SubColorHex;

        if (spaceBody.RingSystem != null)
        {
            ringDistanceInfoValueTextB.text = spaceBody.RingSystem.RingDistance.ToString();
            ringSizeInfoValueTextB.text = spaceBody.RingSystem.RingSize.ToString();
            ringMainColorInfoValueTextB.text = spaceBody.RingSystem.RingMainColorHex.ToString();
            ringSubColorInfoValueTextB.text = spaceBody.RingSystem.RingSubColorHex.ToString();
            ringDetailColorInfoValueTextB.text = spaceBody.RingSystem.RingDetailColorHex.ToString();
        }
    }

    public void SetAstronomerValues(Astronomer astronomer)
    {
        nameInfoValueTextA.text = astronomer.Name;
        dateOfBirthInfoValueTextA.text = astronomer.DateOfBirth.Date.ToString();
        birthPlaceInfoValueTextA.text = astronomer.BirthPlace;
        occupationInfoValueTextA.text = astronomer.Occupation.ToString();
        descriptionInfoValueTextA.text = astronomer.Description;
        marriedInfoValueTextA.text = astronomer.Married.ToString();
        genderInfoValueTextA.text = astronomer.Gender.ToString();
        telescopeAmountInfoValueTextA.text = astronomer.TelescopeAmount.ToString();
    }
}
