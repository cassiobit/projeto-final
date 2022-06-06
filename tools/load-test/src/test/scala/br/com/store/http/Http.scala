package br.com.store.http

import br.com.store.utils.Config
import io.gatling.core.Predef._
import io.gatling.http.Predef._

object Http {
  val httpProtocol = http
    .baseUrls(Config.uris.api_url)
    .inferHtmlResources()
    .acceptHeader("image/webp,*/*")
    .contentTypeHeader("application/json")
    .acceptEncodingHeader("gzip, deflate, br")
    .acceptLanguageHeader("pt-BR,en-US;q=0.8,pt;q=0.5,en;q=0.3")
    .doNotTrackHeader("1")
    .userAgentHeader("Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:76.0) Gecko/20100101 Firefox/76.0")
    .check(status.in(200, 201, 202, 204))
//    .proxy(Proxy("localhost", 8888).httpsPort(8888)) // habilita proxy (fiddler), útil para debug
}