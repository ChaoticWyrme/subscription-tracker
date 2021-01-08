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

const logSite = async () => {
  // URL cannot be empty (e.g. opening new tab)

  let storage = await browser.storage.local.get(["subTimes", "currentTabData", "data"]);

  let subTimes = storage["subTimes"] || [];
  let data = storage["data"] || [];
  let currentTab = storage["currentTabData"];
  if (currentTab !== undefined) {
    currentTab.end = new Date();
    subTimes.push(currentTab);
    let target = data.find((entry) => entry.url === currentTab.domain);
    let sessTime = Math.floor((currentTab.end - currentTab.start) / 1000);
    if (target !== undefined) {
      target.time += sessTime;
    } else {
      data.push({ url: currentTab.domain, time: sessTime });
    }
    console.log("Leaving " + currentTab.domain);
    console.log(data);
  }

  let querying = await browser.tabs.query({ currentWindow: true, active: true });
  let fullURL = querying[0].url;
  let components = fullURL.split("//");
  // URL must have a scheme (e.g. https://)
  if (components.length !== 1) {
    let domain = components[1].split("/")[0];
    currentTab = {
      domain: domain,
      start: new Date(),
    };
    console.log("Accessing " + currentTab.domain + " at " + currentTab.start);
  } else {
    currentTab = undefined;
  }

  await browser.storage.local.set({
    subTimes,
    currentTabData: currentTab,
    data,
  });
};

browser.tabs.onActivated.addListener(logSite);
browser.webNavigation.onHistoryStateUpdated.addListener(logSite);
