import requests
from requests.packages.urllib3.exceptions import InsecureRequestWarning
import json

requests.packages.urllib3.disable_warnings(InsecureRequestWarning)

URL: str = 'https://localhost:7173/api/HealthCheck/'
HEADERS: dict[str, str] = {"accept": "application/json", "Content-Type": "application/json"}

def test_success_response():
    data = {
      "measurements": [
        {
            "type": "TEMP",
            "value": 37
        },
        {
            "type": "HR",
            "value": 60
        },
        {
            "type": "RR",
            "value": 5
        }
      ]
    }
    response = requests.post(URL, data=json.dumps(data), headers=HEADERS, verify=False)
    response_text = response.text
    assert json.loads(response_text) == {"score":3,"errorMessage":"","statusCode":"OK"}


def test_duplicate_measurement():
    data = {
      "measurements": [
        {
            "type": "TEMP",
            "value": 37
        },
        {
            "type": "TEMP",
            "value": 60
        },
      ]
    }
    response = requests.post(URL, data=json.dumps(data), headers=HEADERS, verify=False)
    response_text = response.text
    assert json.loads(response_text) == {"score":-1,"errorMessage":"Duplicate measurement types provided","statusCode":"BadRequest"}

def test_invalid_temperature_value():
    data = {
      "measurements": [
        {
            "type": "TEMP",
            "value": 50,
        },
      ]
    }
    response = requests.post(URL, data=json.dumps(data), headers=HEADERS, verify=False)
    response_text = response.text
    assert json.loads(response_text) == {
        "score":-1,
        "errorMessage":"Value 50 for TEMP is invalid. Please enter a value between 32 and 42.",
        "statusCode":"BadRequest"
    }

def test_invalid_heartrate_value():
    data = {
      "measurements": [
        {
            "type": "HR",
            "value": 10,
        },
      ]
    }
    response = requests.post(URL, data=json.dumps(data), headers=HEADERS, verify=False)
    response_text = response.text
    assert json.loads(response_text) == {"score":-1,"errorMessage":"Value 10 for HR is invalid. Please enter a value between 26 and 220.","statusCode":"BadRequest"}

def test_invalid_respiratory_rate_value():
    data = {
      "measurements": [
        {
            "type": "RR",
            "value": 70,
        },
      ]
    }
    response = requests.post(URL, data=json.dumps(data), headers=HEADERS, verify=False)
    response_text = response.text
    assert json.loads(response_text) == {"score":-1,"errorMessage":"Value 70 for RR is invalid. Please enter a value between 4 and 60.","statusCode":"BadRequest"}
  
def test_invalid_measurement():
    data = {
      "measurements": [
        {
            "type": "INVALID",
            "value": 70,
        },
      ]
    }
    response = requests.post(URL, data=json.dumps(data), headers=HEADERS, verify=False)
    response_text = response.text
    assert json.loads(response_text)['errors'] == {'healthCheck': ['The healthCheck field is required.'], '$.measurements[0].type': ['The JSON value could not be converted to MeasurementType. Path: $.measurements[0].type | LineNumber: 0 | BytePositionInLine: 36.']}

test_success_response();
test_duplicate_measurement();
test_invalid_temperature_value();
test_invalid_heartrate_value();
test_invalid_respiratory_rate_value();
test_invalid_measurement();