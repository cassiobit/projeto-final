
package br.com.store.scenario

import br.com.store.api.{CustomersApi}
import br.com.store.utils.{Config, Utils, SessionKeys}
import io.gatling.core.Predef.{exec, _}
import scala.util.Random

object AddNewCustomer {
  val addNewCustomerFeeder = csv(s"../resources/data/addNewCustomer.csv").circular
  
  // private[cenario] def addNewCustomerFlow() = {
  //   exec(
  //     CustomersApi.addNewCustomer()
  //   )
  // }

   val cenario = scenario("AddNewCustomer Scenario")
    .feed(addNewCustomerFeeder)
    .exec(CustomersApi.addNewCustomer())
}
