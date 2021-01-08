/*
Array of objects, each object having the following properties:
  - domain
  - start
  - end
*/

/*
  - enter: create new object with start time
  - exit out: create new object with end time
*/

const API_URL = "http://chaoticwyrme-001-site1.itempurl.com";

const logSite = async () => {
  // URL cannot be empty (e.g. opening new tab)

  let storage = await browser.storage.local.get(["subTimes", "currentTabData"]);

  let subTimes = storage["subTimes"] || [];
  let currentTab = storage["currentTabData"];
  if (currentTab !== undefined) {
    currentTab.endTime = new Date();
    subTimes.push(currentTab);
    console.log("Leaving " + currentTab.domain);
  }

  let querying = await browser.tabs.query({ currentWindow: true, active: true });
  let fullURL = querying[0].url;
  let components = fullURL.split("//");
  // URL must have a scheme (e.g. https://)
  if (components.length !== 1) {
    let domain = components[1].split("/")[0];
    currentTab = {
      domain: domain,
      startTime: new Date(),
    };
    console.log("Accessing " + currentTab.domain + " at " + currentTab.startTime);
  } else {
    currentTab = undefined;
  }

  fetch(API_URL + "/api/TimeData/user", {
    method: "POST",
    body: JSON.stringify(currentTab),
  }).then(res => res.json()).then(res => console.log("Success: " + res););

  await browser.storage.local.set({
    subTimes,
    currentTabData: currentTab,
  });
};

browser.tabs.onActivated.addListener(logSite);
browser.webNavigation.onHistoryStateUpdated.addListener(logSite);
