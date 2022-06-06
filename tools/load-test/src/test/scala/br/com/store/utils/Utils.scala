package br.com.store.utils

import com.typesafe.scalalogging.StrictLogging
import io.gatling.core.Predef._
import io.gatling.core.session.Session
import io.gatling.core.structure.ChainBuilder

object Utils extends StrictLogging {
  private val IsDebugEnabled = logger.underlying.isDebugEnabled

  def getProperty(propertyName: String, defaultValue: String) = {
    Option(System.getenv(propertyName))
      .orElse(Option(System.getProperty(propertyName)))
      .getOrElse(defaultValue)
  }

  def printKey(key: String): ChainBuilder = {
    exec(session => {
      if (IsDebugEnabled) {
        println("Imprimindo key [" + key + "]: " + getKey(session, key))
      }
      session
    })
  }

  def getKey(session: Session, keyName: String): String = {
    keyExtractor(session.attributes.get(keyName))
  }

  def keyExtractor(keyOption: Option[Any]): String = {
    keyExtractor(keyOption, true)
  }

  def keyExtractor(keyOption: Option[Any], end: Boolean): String = {
    val key = keyOption.toString()
    if (key.contains("Some(")) {
      if (end)
        key.substring(5).replace(")", "")
      else key.substring(5).replace(")", ",")
    } else "NULL"
  }

  def printSomething(value: String): ChainBuilder =  {
    exec(session => {
      if (IsDebugEnabled) {
        println(value)
      }
      session
    })
  }

  def printSessionAttributes(): ChainBuilder = {
    exec(session => {
      println(session.attributes)
      session
    })
  }
}