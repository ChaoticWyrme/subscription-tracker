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

browser.tabs.onActivated.addListener(async () => {
  let querying = await browser.tabs.query({ currentWindow: true, active: true });
  let fullURL = querying[0].url;
  // URL cannot be empty (e.g. opening new tab)
  if (fullURL !== "") {
    let components = fullURL.split("//");
    // URL must have a scheme (e.g. https://)
    if (components.length !== 1) {
      let domain = components[1].split("/")[0];
      let storage = await browser.storage.local.get("subTimes");

      storage = await browser.storage.local.get("subTimes");
      storage.subTimes.push({ domain: domain, start: new Date(), end: -1 });
      await browser.storage.local.set({
        subTimes: storage.subTimes,
      });

      // logging purposes
      storage = await browser.storage.local.get("subTimes");
      let target = storage.subTimes[0];
      console.log("Accessing " + target.domain + " at " + target.start);
    }
  }
});
