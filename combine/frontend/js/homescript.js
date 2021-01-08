

//make the subscription boxes
//async function makesubboxes(){
const makesubboxes = async () => {
    console.log("before");
    let currentsubs = await browser.storage.local.get(["usersubscriptions"]);
    console.log(currentsubs);
    if(!isEmpty(currentsubs)){
        for(const subid of currentsubs){
            document.getElementById("subscriptions").innerHTML += `
                <div class="subscriptionbox" id="${subid}">
                    <div class="subscriptionleft">
                        <img src="icon.png" class="subscriptionicon" width="60" height="60">

                        <p class="subscriptionname">${subid}</p>
                    </div>
                    <div class="subscriptionright">
                        <p class="subscriptionprice">$10.99</p>
                                            <a href="#"><img src="images/graph.png" class="graphIcon" width="30" height="30"></a>

                        <button type="button" class="close" aria-label="Close" onclick="removesub(subid)">
                            <span aria-hidden="true">&times;</span>
                        </button>

                    </div>
            
            `
        }
    } else {
        document.getElementById("subscriptions").innerHTML = `hello`;
    }
    
    
}

makesubboxes();

function isEmpty(obj) {
    for(var prop in obj) {
        if(obj.hasOwnProperty(prop))
            return false;
    }

    return true;
}

//remove subscription
async function removesub(subid){
    let currentsubs = await browser.storage.local.get("usersubscriptions");

    let index = currentsubs.indexOf(subid);
    if (index !== -1) {
        currentsubs.splice(index, 1);
    }

    await browser.storage.local.set({"usersubscriptions":currentsubs});

}
