package br.com.store.api

import br.com.store.http.Header
import br.com.store.utils.{SessionKeys}
import io.gatling.core.Predef._
import io.gatling.http.Predef._

object CustomersApi {
  val baseUrl = "/customers";

  def addNewCustomer() = {
    exec(
      http("store-api: addNewCustomer")
        .post(baseUrl)
        .body(ElFileBody("bodies/customers/addNewCustomer.json"))
        .check(status.in(201),
          status.saveAs(SessionKeys.responseStatus))
    )
  }
}