
// Store all evangelists here
var evangelistArr = [];

// Object template for evangelsits
objEvangelist = {
    Amanda: {
        sName: "",
        arrCommitments: [],
        nTotalCommitments: 0,
        nIncCommitments: 0,
        nComCommitments: 0,
        bCrm: false,
        bFri: false,
        bExp: false
    },
    Blain: {
        sName: "",
        arrCommitments: [],
        nTotalCommitments: 0,
        nIncCommitments: 0,
        nComCommitments: 0,
        bCrm: false,
        bFri: false,
        bExp: false
    },
    Dave: {
        sName: "",
        arrCommitments: [],
        nTotalCommitments: 0,
        nIncCommitments: 0,
        nComCommitments: 0,
        bCrm: false,
        bFri: false,
        bExp: false
    },
    David: {
        sName: "",
        arrCommitments: [],
        nTotalCommitments: 0,
        bCrm: false,
        bFri: false,
        bExp: false
    },
    Ian: {
        sName: "",
        arrCommitments: [],
        nTotalCommitments: 0,
        nIncCommitments: 0,
        nComCommitments: 0,
        bCrm: false,
        bFri: false,
        bExp: false
    },
    Kristin: {
        sName: "",
        arrCommitments: [],
        nTotalCommitments: 0,
        nIncCommitments: 0,
        nComCommitments: 0,
        bCrm: false,
        bFri: false,
        bExp: false
    },
    Mostafa: {
        sName: "",
        arrCommitments: [],
        nTotalCommitments: 0,
        nIncCommitments: 0,
        nComCommitments: 0,
        bCrm: false,
        bFri: false,
        bExp: false
    },
    Shahed: {
        sName: "",
        arrCommitments: [],
        nTotalCommitments: 0,
        nIncCommitments: 0,
        nComCommitments: 0,
        bCrm: false,
        bFri: false,
        bExp: false
    },
    Tommy: {
        sName: "",
        arrCommitments: [],
        nTotalCommitments: 0,
        nIncCommitments: 0,
        nComCommitments: 0,
        bCrm: false,
        bFri: false,
        bExp: false
    },
    Joe: {
        sName: "",
        arrCommitments: [],
        nTotalCommitments: 0,
        nIncCommitments: 0,
        nComCommitments: 0,
        bCrm: false,
        bFri: false,
        bExp: false
    }
};
// Pre-populate array w/ empty evangelist data
evangelistArr.push(objEvangelist);

// Sets the current object to an evangelist object, based on the name pulled via Ajax
function setCurrentEvangelist(name) {
    var objCurrent = objEvangelist;

    switch (name) {
        case "Amanda Lange": {
            objCurrent = objEvangelist.Amanda;
            break;
        }
        case "Blain Barton": {
            objCurrent = objEvangelist.Blain;
            break;
        }
        case "Dave Voyles": {
            objCurrent = objEvangelist.Dave;
            break;
        }
        case "David Crook": {
            objCurrent = objEvangelist.David;
            break;
        }
        case "Ian Philpot": {
            objCurrent = objEvangelist.Ian;
            break;
        }
        case "Kristin Ottofy": {
            objCurrent = objEvangelist.Blain;
            break;
        }
        case "Mostafa Elzoghbi": {
            objCurrent = objEvangelist.Mostafa;
            break;
        }
        case "Shahed Chowhuri": {
            objCurrent = objEvangelist.Shahed;
            break;
        }
        case "Joe Raio": {
            objCurrent = objEvangelist.Joe;
            break;
        }
    }
    return objCurrent;
}


// AJAX call to return commitments from DB
function InsertToLocalArray() {
    $.getJSON('commitments/GetCommitmentsList', function (data) {
        $.each(data, function (i) {
            // Set name and create new array to store commitments
            var evangelist = setCurrentEvangelist(data[i].fullName);
            var arrCommitments = [{
                commitment: "",
                status: 0
            }];

            // Set commitments, increment total complete or incomplete commitments
            arrCommitments.commitment = data[i].title;
            arrCommitments.status = data[i].status;
            if (data[i].status === 0) {
                evangelist.nIncCommitments++;
            } else {
                evangelist.nComCommitments++;
            }
            evangelist.nTotalCommitments++;
            evangelist.arrCommitments.push(arrCommitments);

            // DEBUG
            console.log(evangelistArr);
        });
    });
};

InsertToLocalArray();