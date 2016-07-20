
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