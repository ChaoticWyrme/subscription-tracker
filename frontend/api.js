const API_URL = "http://localhost:5000/api";

function apiPost(path, data) {
  return fetch(API_URL + path, {
    method: 'POST',
    body: JSON.stringify(data),
    headers: {
      'Content-Type': 'application/json;charset=UTF-8',
      'Accept': 'application/json, text/plain'
    }
  }).then(data => data.json());
}

function apiGet(path) {
  return fetch(API_URL + path, {
    headers: {
      'Accept': 'application/json, text/plain'
    }
  }).then(data => data.json());
}

function encodeDate(date) {
  return encodeURIComponent(date.toISOString())
}

function addService(name, price, url) {
  return apiPost('/Services', {
    name,
    price,
    url
  });
}

function subscribe(serviceId, expireDate) {
  if (!expireDate instanceof Date || expireDate == undefined) {
    expireDate = new Date(new Date().getTime() + new Date(5 * 1000 * 60).getTime());
  }
  return apiGet(`/Subscription/${serviceId}/Subscribe?username=user&expireDate=${encodeDate(expireDate)}`);
}

function fakeTime(url, timeSpanSeconds) {
  let startTime = new Date();
  let endTime = new Date(new Date().getTime() + timeSpanSeconds * 1000);

  let timeData = {
    url,
    startTime: encodeDate(startTime),
    endTime: encodeDate(endTime)
  };
  apiPost('/TimeData/user', timeData);
}


function initializeServer() {
  apiPost('/Services/MassCreate', [
    {
      "name": "Netflix",
      "price": 8.99,
      "url": "netflix.com"
    },
    {
      "name": "Hulu",
      "price": 5.99,
      "url": "hulu.com"
    },
    {
      "name": "HBO Max",
      "price": 14.99,
      "url": "hbomax.com"
    }
  ]);

  subscribe(1);
  subscribe(2);
  subscribe(3);
}

class Stats {
  totalTimeSite(url) {
    return apiGet(`/Stats/TotalTime?url=${url}&username=user`);
  }

  totalTimeAllSites() {
    return apiGet('/Stats/AllSites/TotalTime?username=user')
  }

}
