/** Store all evangelists here */
var evangelistArr = [];

/** Object template for evangelsits*/
objEvangelist = {
    Amanda: {
        sName: "",
        arrCommitments: [],
        nTotalCommitments: 0,
        nIncCommitments: 0,
        nComCommitments: 0,
        bCrm: false,
        bFri: false,
        bExp: false,
        nTEDeliveredSessions: 0,
        nMVPSessions: 0,
    },
    Blain: {
        sName: "",
        arrCommitments: [],
        nTotalCommitments: 0,
        nIncCommitments: 0,
        nComCommitments: 0,
        bCrm: false,
        bFri: false,
        bExp: false,
        nTEDeliveredSessions: 0,
        nMVPSessions: 0,
    },
    Dave: {
        sName: "",
        arrCommitments: [],
        nTotalCommitments: 0,
        nIncCommitments: 0,
        nComCommitments: 0,
        bCrm: false,
        bFri: false,
        bExp: false,
        nTEDeliveredSessions: 0,
        nMVPSessions: 0,
    },
    David: {
        sName: "",
        arrCommitments: [],
        nTotalCommitments: 0,
        bCrm: false,
        bFri: false,
        bExp: false,
        nTEDeliveredSessions: 0,
        nMVPSessions: 0,
    },
    Ian: {
        sName: "",
        arrCommitments: [],
        nTotalCommitments: 0,
        nIncCommitments: 0,
        nComCommitments: 0,
        bCrm: false,
        bFri: false,
        bExp: false,
        nTEDeliveredSessions: 0,
        nMVPSessions: 0,
    },
    Kristin: {
        sName: "",
        arrCommitments: [],
        nTotalCommitments: 0,
        nIncCommitments: 0,
        nComCommitments: 0,
        bCrm: false,
        bFri: false,
        bExp: false,
        nTEDeliveredSessions: 0,
        nMVPSessions: 0,
    },
    Mostafa: {
        sName: "",
        arrCommitments: [],
        nTotalCommitments: 0,
        nIncCommitments: 0,
        nComCommitments: 0,
        bCrm: false,
        bFri: false,
        bExp: false,
        nTEDeliveredSessions: 0,
        nMVPSessions: 0,
    },
    Shahed: {
        sName: "",
        arrCommitments: [],
        nTotalCommitments: 0,
        nIncCommitments: 0,
        nComCommitments: 0,
        bCrm: false,
        bFri: false,
        bExp: false,
        nTEDeliveredSessions: 0,
        nMVPSessions: 0,
    },
    Tommy: {
        sName: "",
        arrCommitments: [],
        nTotalCommitments: 0,
        nIncCommitments: 0,
        nComCommitments: 0,
        bCrm: false,
        bFri: false,
        bExp: false,
        nTEDeliveredSessions: 0,
        nMVPSessions: 0,
    },
    Joe: {
        sName: "",
        arrCommitments: [],
        nTotalCommitments: 0,
        nIncCommitments: 0,
        nComCommitments: 0,
        bCrm: false,
        bFri: false,
        bExp: false,
        nTEDeliveredSessions: 0,
        nMVPSessions: 0,
    }
};
/** Pre-populate array w/ empty evangelist data 
 * @param {object} objectEvangelist - Pass in the template for the evangelist data structure
 */
evangelistArr.push(objEvangelist);


/**  Sets the current object to an evangelist object, based on the name pulled via Ajax
 * @param {string} name - Which evangelist do we want to set as the current object?
 */
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

