  google.charts.load('current', {'packages':['bar']});
      google.charts.setOnLoadCallback(drawAscendChart);
      google.charts.setOnLoadCallback(drawCommChart);
      google.charts.setOnLoadCallback(drawWeeksChart);


      function drawCommChart() {
          var data = google.visualization.arrayToDataTable([
            ['Months'  , 'BASELINE', 'MVP', 'Blain', 'Amanda', 'Dave', 'Joe', 'David', 'Kristin', 'Shahed', 'Mostafa'],
            ['July'    , 25, 12, 32, 28, 15, 10, 20, 15, 17, 12],
            ['Aug'     , 25, 16, 32, 28, 15, 10, 20, 15, 17, 12],
            ['Sept'    , 25, 13, 2, 28, 15, 10, 20, 15, 17, 12],
            ['Q1 total', 75, 41, 96, 84, 45, 30, 60, 45, 51, 36],
          ]);

          var options= {
              chart: {
                  title   : 'Community Actual Metrics',
                  subtitle: 'TE & MVP delivered sessions',
              }
          };

          var comm_actual_metric = new google.charts.Bar(document.getElementById('community_actual_metric'));
              comm_actual_metric.draw(data, options);
      };

      function drawAscendChart() {
          var data = google.visualization.arrayToDataTable([
            ['Months'                  , 'Blain', 'Amanda', 'Dave', 'Joe', 'David', 'Kristin', 'Shahed', 'Mostafa'],
            ['Ascend lead'             ,  4,  4,  3,  2,  2,  2,  2,  2],
            ['Ascend member'           ,  3,  4,  3,  2,  2,  2,  3,  2],
          ]);

          var options = {
              chart: {
                  title   : 'Ascend Actual Metrics',
                  subtitle: 'Lead & member roles, + weeks on project',
              }
          };

          var ascend_actual_metric = new google.charts.Bar(document.getElementById('ascend_actual_metric'));
              ascend_actual_metric.draw(data, options);
      };


      function drawWeeksChart() {
          var data = google.visualization.arrayToDataTable([
            ['Months', 'Blain', 'Amanda', 'Dave', 'Joe', 'David', 'Kristin', 'Shahed', 'Mostafa'],
            ['Weeks on proj', 21, 20, 21, 18, 18, 18, 15, 18],
            ['Weeks left for community', 15, 16, 15, 17, 17, 17, 22, 17],
          ]);

          var options = {
              chart: {
                  title: 'Weeks remaining for work',
                  subtitle: 'Split between ascend+ and community',
              }
          };

          var weeks_metric = new google.charts.Bar(document.getElementById('weeks_metric'));
              weeks_metric.draw(data, options);
      };  