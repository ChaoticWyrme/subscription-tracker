document.querySelector(".add_subscription_form").addEventListener("submit", async (e) => {
  //make post request
  e.preventDefault();

  let storage = await browser.storage.local.get(["subList"]);

  let subList = storage["subList"] || [];

  let name = document.getElementById("sub_name").value;
  let price = document.getElementById("price").value;
  let url = document.getElementById("sub_url").value;

  subList.push({
    name,
    price,
    url,
  });

  console.log(subList);

  // const xhr = new XMLHttpRequest();
  // xhr.open("POST", "http://chaoticwyrme-001-site1.itempurl.com/");
  // xhr.onreadystatechange = function () {
  //   if (xhr.readyState === xhr.DONE) {
  //     window.location.href = "home.html";
  //   }
  // };
  // xhr.send(data);

  window.location.href = "home.html";

  document.getElementById("subscriptions").innerHTML += `<div id="subscriptions">
              <div class="subscriptionbox">
                  <div class="subscriptionleft">
                      <img src="icon.png" class="subscriptionicon" width="60" height="60">
                      <p class="subscriptionname">${name}</p>
                  </div>
                  <div class="subscriptionright">
                      <p class="subscriptionprice">$${price}</p>
                      <a href="#"><img src="images/graph.png" class="graphIcon" width="30" height="30"></a>
                      <button type="button" class="close" aria-label="Close">
                          <span aria-hidden="true">&times;</span>
                      </button>
                  </div>
              </div>
            </div>`;
});
