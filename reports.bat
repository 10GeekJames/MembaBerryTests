livingdoc feature-folder ./ -t ./bin/Debug/net8.0/TestExecution* --output ./Reports/TestFeatureReport.html

livingdoc feature-folder ./ --output-type JSON -t ./bin/Debug/net8.0/TestExecution*.json --output ./Reports/TestFeatureData.json

explorer .\Reports