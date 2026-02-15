# GPL-3.0 License (FORBIDDEN)
source 'https://rubygems.org'

ruby '2.3.0' # EOL Ruby — Trivy flag

# Known vulnerable gems
gem 'rails', '4.2.0'        # CVE-2015-7576, CVE-2016-6316
gem 'rack', '1.6.0'         # CVE-2018-16470
gem 'nokogiri', '1.6.6'     # CVE-2017-9050
gem 'json', '1.8.1'         # CVE-2020-10663
gem 'devise', '3.2.4'       # multiple CVEs
gem 'rest-client', '1.6.7'  # CVE-2015-1820
gem 'webrick', '1.3.1'      # CVE-2020-25613

