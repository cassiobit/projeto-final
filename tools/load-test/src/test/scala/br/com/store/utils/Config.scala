package br.com.store.utils

import scala.concurrent.duration.DurationInt

object Config {
  object uris {
    var api_url = ""
  }

  object pause {
    var user_input = 1.second
    var user_read = 5.second
  }
}