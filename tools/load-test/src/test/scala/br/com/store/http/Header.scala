package br.com.store.http

import br.com.store.utils.{Config, SessionKeys}

/**
 * Cabeçalhos http comuns à aplicação.
 */
object Header {
  val accept_application_json = Map(
    "Accept" -> "application/json, text/plain, */*"
  )

  val no_cache = Map(
    "Cache-Control" -> "no-cache",
    "Pragma" -> "no-cache"
  )

  val accept_encoding_gzip_deflate_br = Map(
    "Accept-Encoding" -> "gzip, deflate, br"
  )

  val content_type_application_json = Map(
    "Content-Type" -> "application/json"
  )

  val keep_alive = Map(
    "Connection" -> "keep-alive"
  )

  val authorization = Map(
    "Authorization" -> "Bearer ".concat("${" + SessionKeys.bearer_token + "}")
  )

  val origin = Map(
    "Origin" -> Config.uris.api_url
  )

  val accepted_language_en = Map(
    "accept-language" -> "en"
  )
}