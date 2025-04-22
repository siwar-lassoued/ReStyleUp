// ============================================================================
//
// Copyright (c) 2006-2015, Talend SA
//
// Ce code source a été automatiquement généré par_Talend Open Studio for Data Integration
// / Soumis à la Licence Apache, Version 2.0 (la "Licence") ;
// votre utilisation de ce fichier doit respecter les termes de la Licence.
// Vous pouvez obtenir une copie de la Licence sur
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Sauf lorsqu'explicitement prévu par la loi en vigueur ou accepté par écrit, le logiciel
// distribué sous la Licence est distribué "TEL QUEL",
// SANS GARANTIE OU CONDITION D'AUCUNE SORTE, expresse ou implicite.
// Consultez la Licence pour connaître la terminologie spécifique régissant les autorisations et
// les limites prévues par la Licence.

package restyle_up.unite_files_0_1;

import routines.Numeric;
import routines.DataOperation;
import routines.TalendDataGenerator;
import routines.TalendStringUtil;
import routines.TalendString;
import routines.StringHandling;
import routines.Relational;
import routines.TalendDate;
import routines.Mathematical;
import routines.system.*;
import routines.system.api.*;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;
import java.math.BigDecimal;
import java.io.ByteArrayOutputStream;
import java.io.ByteArrayInputStream;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.ObjectOutputStream;
import java.io.ObjectInputStream;
import java.io.IOException;
import java.util.Comparator;

@SuppressWarnings("unused")

/**
 * Job: unite_files Purpose: <br>
 * Description: <br>
 * 
 * @author user@talend.com
 * @version 8.0.1.20211109_1610
 * @status
 */
public class unite_files implements TalendJob {

	protected static void logIgnoredError(String message, Throwable cause) {
		System.err.println(message);
		if (cause != null) {
			cause.printStackTrace();
		}

	}

	public final Object obj = new Object();

	// for transmiting parameters purpose
	private Object valueObject = null;

	public Object getValueObject() {
		return this.valueObject;
	}

	public void setValueObject(Object valueObject) {
		this.valueObject = valueObject;
	}

	private final static String defaultCharset = java.nio.charset.Charset.defaultCharset().name();

	private final static String utf8Charset = "UTF-8";

	// contains type for every context property
	public class PropertiesWithType extends java.util.Properties {
		private static final long serialVersionUID = 1L;
		private java.util.Map<String, String> propertyTypes = new java.util.HashMap<>();

		public PropertiesWithType(java.util.Properties properties) {
			super(properties);
		}

		public PropertiesWithType() {
			super();
		}

		public void setContextType(String key, String type) {
			propertyTypes.put(key, type);
		}

		public String getContextType(String key) {
			return propertyTypes.get(key);
		}
	}

	// create and load default properties
	private java.util.Properties defaultProps = new java.util.Properties();

	// create application properties with default
	public class ContextProperties extends PropertiesWithType {

		private static final long serialVersionUID = 1L;

		public ContextProperties(java.util.Properties properties) {
			super(properties);
		}

		public ContextProperties() {
			super();
		}

		public void synchronizeContext() {

		}

		// if the stored or passed value is "<TALEND_NULL>" string, it mean null
		public String getStringValue(String key) {
			String origin_value = this.getProperty(key);
			if (NULL_VALUE_EXPRESSION_IN_COMMAND_STRING_FOR_CHILD_JOB_ONLY.equals(origin_value)) {
				return null;
			}
			return origin_value;
		}

	}

	protected ContextProperties context = new ContextProperties(); // will be instanciated by MS.

	public ContextProperties getContext() {
		return this.context;
	}

	private final String jobVersion = "0.1";
	private final String jobName = "unite_files";
	private final String projectName = "RESTYLE_UP";
	public Integer errorCode = null;
	private String currentComponent = "";

	private final java.util.Map<String, Object> globalMap = new java.util.HashMap<String, Object>();
	private final static java.util.Map<String, Object> junitGlobalMap = new java.util.HashMap<String, Object>();

	private final java.util.Map<String, Long> start_Hash = new java.util.HashMap<String, Long>();
	private final java.util.Map<String, Long> end_Hash = new java.util.HashMap<String, Long>();
	private final java.util.Map<String, Boolean> ok_Hash = new java.util.HashMap<String, Boolean>();
	public final java.util.List<String[]> globalBuffer = new java.util.ArrayList<String[]>();

	private RunStat runStat = new RunStat();

	// OSGi DataSource
	private final static String KEY_DB_DATASOURCES = "KEY_DB_DATASOURCES";

	private final static String KEY_DB_DATASOURCES_RAW = "KEY_DB_DATASOURCES_RAW";

	public void setDataSources(java.util.Map<String, javax.sql.DataSource> dataSources) {
		java.util.Map<String, routines.system.TalendDataSource> talendDataSources = new java.util.HashMap<String, routines.system.TalendDataSource>();
		for (java.util.Map.Entry<String, javax.sql.DataSource> dataSourceEntry : dataSources.entrySet()) {
			talendDataSources.put(dataSourceEntry.getKey(),
					new routines.system.TalendDataSource(dataSourceEntry.getValue()));
		}
		globalMap.put(KEY_DB_DATASOURCES, talendDataSources);
		globalMap.put(KEY_DB_DATASOURCES_RAW, new java.util.HashMap<String, javax.sql.DataSource>(dataSources));
	}

	public void setDataSourceReferences(List serviceReferences) throws Exception {

		java.util.Map<String, routines.system.TalendDataSource> talendDataSources = new java.util.HashMap<String, routines.system.TalendDataSource>();
		java.util.Map<String, javax.sql.DataSource> dataSources = new java.util.HashMap<String, javax.sql.DataSource>();

		for (java.util.Map.Entry<String, javax.sql.DataSource> entry : BundleUtils
				.getServices(serviceReferences, javax.sql.DataSource.class).entrySet()) {
			dataSources.put(entry.getKey(), entry.getValue());
			talendDataSources.put(entry.getKey(), new routines.system.TalendDataSource(entry.getValue()));
		}

		globalMap.put(KEY_DB_DATASOURCES, talendDataSources);
		globalMap.put(KEY_DB_DATASOURCES_RAW, new java.util.HashMap<String, javax.sql.DataSource>(dataSources));
	}

	private final java.io.ByteArrayOutputStream baos = new java.io.ByteArrayOutputStream();
	private final java.io.PrintStream errorMessagePS = new java.io.PrintStream(new java.io.BufferedOutputStream(baos));

	public String getExceptionStackTrace() {
		if ("failure".equals(this.getStatus())) {
			errorMessagePS.flush();
			return baos.toString();
		}
		return null;
	}

	private Exception exception;

	public Exception getException() {
		if ("failure".equals(this.getStatus())) {
			return this.exception;
		}
		return null;
	}

	private class TalendException extends Exception {

		private static final long serialVersionUID = 1L;

		private java.util.Map<String, Object> globalMap = null;
		private Exception e = null;
		private String currentComponent = null;
		private String virtualComponentName = null;

		public void setVirtualComponentName(String virtualComponentName) {
			this.virtualComponentName = virtualComponentName;
		}

		private TalendException(Exception e, String errorComponent, final java.util.Map<String, Object> globalMap) {
			this.currentComponent = errorComponent;
			this.globalMap = globalMap;
			this.e = e;
		}

		public Exception getException() {
			return this.e;
		}

		public String getCurrentComponent() {
			return this.currentComponent;
		}

		public String getExceptionCauseMessage(Exception e) {
			Throwable cause = e;
			String message = null;
			int i = 10;
			while (null != cause && 0 < i--) {
				message = cause.getMessage();
				if (null == message) {
					cause = cause.getCause();
				} else {
					break;
				}
			}
			if (null == message) {
				message = e.getClass().getName();
			}
			return message;
		}

		@Override
		public void printStackTrace() {
			if (!(e instanceof TalendException || e instanceof TDieException)) {
				if (virtualComponentName != null && currentComponent.indexOf(virtualComponentName + "_") == 0) {
					globalMap.put(virtualComponentName + "_ERROR_MESSAGE", getExceptionCauseMessage(e));
				}
				globalMap.put(currentComponent + "_ERROR_MESSAGE", getExceptionCauseMessage(e));
				System.err.println("Exception in component " + currentComponent + " (" + jobName + ")");
			}
			if (!(e instanceof TDieException)) {
				if (e instanceof TalendException) {
					e.printStackTrace();
				} else {
					e.printStackTrace();
					e.printStackTrace(errorMessagePS);
					unite_files.this.exception = e;
				}
			}
			if (!(e instanceof TalendException)) {
				try {
					for (java.lang.reflect.Method m : this.getClass().getEnclosingClass().getMethods()) {
						if (m.getName().compareTo(currentComponent + "_error") == 0) {
							m.invoke(unite_files.this, new Object[] { e, currentComponent, globalMap });
							break;
						}
					}

					if (!(e instanceof TDieException)) {
					}
				} catch (Exception e) {
					this.e.printStackTrace();
				}
			}
		}
	}

	public void tFileInputDelimited_1_error(Exception exception, String errorComponent,
			final java.util.Map<String, Object> globalMap) throws TalendException {

		end_Hash.put(errorComponent, System.currentTimeMillis());

		status = "failure";

		tFileInputDelimited_1_onSubJobError(exception, errorComponent, globalMap);
	}

	public void tMap_1_error(Exception exception, String errorComponent, final java.util.Map<String, Object> globalMap)
			throws TalendException {

		end_Hash.put(errorComponent, System.currentTimeMillis());

		status = "failure";

		tFileInputDelimited_1_onSubJobError(exception, errorComponent, globalMap);
	}

	public void tUnite_1_error(Exception exception, String errorComponent,
			final java.util.Map<String, Object> globalMap) throws TalendException {

		end_Hash.put(errorComponent, System.currentTimeMillis());

		status = "failure";

		tFileInputDelimited_1_onSubJobError(exception, errorComponent, globalMap);
	}

	public void tMap_3_error(Exception exception, String errorComponent, final java.util.Map<String, Object> globalMap)
			throws TalendException {

		end_Hash.put(errorComponent, System.currentTimeMillis());

		status = "failure";

		tFileInputDelimited_1_onSubJobError(exception, errorComponent, globalMap);
	}

	public void tDBOutput_1_error(Exception exception, String errorComponent,
			final java.util.Map<String, Object> globalMap) throws TalendException {

		end_Hash.put(errorComponent, System.currentTimeMillis());

		status = "failure";

		tFileInputDelimited_1_onSubJobError(exception, errorComponent, globalMap);
	}

	public void tFileInputExcel_1_error(Exception exception, String errorComponent,
			final java.util.Map<String, Object> globalMap) throws TalendException {

		end_Hash.put(errorComponent, System.currentTimeMillis());

		status = "failure";

		tFileInputDelimited_1_onSubJobError(exception, errorComponent, globalMap);
	}

	public void tMap_2_error(Exception exception, String errorComponent, final java.util.Map<String, Object> globalMap)
			throws TalendException {

		end_Hash.put(errorComponent, System.currentTimeMillis());

		status = "failure";

		tFileInputDelimited_1_onSubJobError(exception, errorComponent, globalMap);
	}

	public void tFileInputDelimited_1_onSubJobError(Exception exception, String errorComponent,
			final java.util.Map<String, Object> globalMap) throws TalendException {

		resumeUtil.addLog("SYSTEM_LOG", "NODE:" + errorComponent, "", Thread.currentThread().getId() + "", "FATAL", "",
				exception.getMessage(), ResumeUtil.getExceptionStackTrace(exception), "");

	}

	public static class out3Struct implements routines.system.IPersistableRow<out3Struct> {
		final static byte[] commonByteArrayLock_RESTYLE_UP_unite_files = new byte[0];
		static byte[] commonByteArray_RESTYLE_UP_unite_files = new byte[0];

		public String Marque;

		public String getMarque() {
			return this.Marque;
		}

		public String Type;

		public String getType() {
			return this.Type;
		}

		public String Couleur;

		public String getCouleur() {
			return this.Couleur;
		}

		public String Taille;

		public String getTaille() {
			return this.Taille;
		}

		public String Matiere;

		public String getMatiere() {
			return this.Matiere;
		}

		public Float Prix_origine;

		public Float getPrix_origine() {
			return this.Prix_origine;
		}

		public String Genre;

		public String getGenre() {
			return this.Genre;
		}

		public String Saison;

		public String getSaison() {
			return this.Saison;
		}

		public String Collection;

		public String getCollection() {
			return this.Collection;
		}

		public String Popularite;

		public String getPopularite() {
			return this.Popularite;
		}

		private String readString(ObjectInputStream dis) throws IOException {
			String strReturn = null;
			int length = 0;
			length = dis.readInt();
			if (length == -1) {
				strReturn = null;
			} else {
				if (length > commonByteArray_RESTYLE_UP_unite_files.length) {
					if (length < 1024 && commonByteArray_RESTYLE_UP_unite_files.length == 0) {
						commonByteArray_RESTYLE_UP_unite_files = new byte[1024];
					} else {
						commonByteArray_RESTYLE_UP_unite_files = new byte[2 * length];
					}
				}
				dis.readFully(commonByteArray_RESTYLE_UP_unite_files, 0, length);
				strReturn = new String(commonByteArray_RESTYLE_UP_unite_files, 0, length, utf8Charset);
			}
			return strReturn;
		}

		private String readString(org.jboss.marshalling.Unmarshaller unmarshaller) throws IOException {
			String strReturn = null;
			int length = 0;
			length = unmarshaller.readInt();
			if (length == -1) {
				strReturn = null;
			} else {
				if (length > commonByteArray_RESTYLE_UP_unite_files.length) {
					if (length < 1024 && commonByteArray_RESTYLE_UP_unite_files.length == 0) {
						commonByteArray_RESTYLE_UP_unite_files = new byte[1024];
					} else {
						commonByteArray_RESTYLE_UP_unite_files = new byte[2 * length];
					}
				}
				unmarshaller.readFully(commonByteArray_RESTYLE_UP_unite_files, 0, length);
				strReturn = new String(commonByteArray_RESTYLE_UP_unite_files, 0, length, utf8Charset);
			}
			return strReturn;
		}

		private void writeString(String str, ObjectOutputStream dos) throws IOException {
			if (str == null) {
				dos.writeInt(-1);
			} else {
				byte[] byteArray = str.getBytes(utf8Charset);
				dos.writeInt(byteArray.length);
				dos.write(byteArray);
			}
		}

		private void writeString(String str, org.jboss.marshalling.Marshaller marshaller) throws IOException {
			if (str == null) {
				marshaller.writeInt(-1);
			} else {
				byte[] byteArray = str.getBytes(utf8Charset);
				marshaller.writeInt(byteArray.length);
				marshaller.write(byteArray);
			}
		}

		public void readData(ObjectInputStream dis) {

			synchronized (commonByteArrayLock_RESTYLE_UP_unite_files) {

				try {

					int length = 0;

					this.Marque = readString(dis);

					this.Type = readString(dis);

					this.Couleur = readString(dis);

					this.Taille = readString(dis);

					this.Matiere = readString(dis);

					length = dis.readByte();
					if (length == -1) {
						this.Prix_origine = null;
					} else {
						this.Prix_origine = dis.readFloat();
					}

					this.Genre = readString(dis);

					this.Saison = readString(dis);

					this.Collection = readString(dis);

					this.Popularite = readString(dis);

				} catch (IOException e) {
					throw new RuntimeException(e);

				}

			}

		}

		public void readData(org.jboss.marshalling.Unmarshaller dis) {

			synchronized (commonByteArrayLock_RESTYLE_UP_unite_files) {

				try {

					int length = 0;

					this.Marque = readString(dis);

					this.Type = readString(dis);

					this.Couleur = readString(dis);

					this.Taille = readString(dis);

					this.Matiere = readString(dis);

					length = dis.readByte();
					if (length == -1) {
						this.Prix_origine = null;
					} else {
						this.Prix_origine = dis.readFloat();
					}

					this.Genre = readString(dis);

					this.Saison = readString(dis);

					this.Collection = readString(dis);

					this.Popularite = readString(dis);

				} catch (IOException e) {
					throw new RuntimeException(e);

				}

			}

		}

		public void writeData(ObjectOutputStream dos) {
			try {

				// String

				writeString(this.Marque, dos);

				// String

				writeString(this.Type, dos);

				// String

				writeString(this.Couleur, dos);

				// String

				writeString(this.Taille, dos);

				// String

				writeString(this.Matiere, dos);

				// Float

				if (this.Prix_origine == null) {
					dos.writeByte(-1);
				} else {
					dos.writeByte(0);
					dos.writeFloat(this.Prix_origine);
				}

				// String

				writeString(this.Genre, dos);

				// String

				writeString(this.Saison, dos);

				// String

				writeString(this.Collection, dos);

				// String

				writeString(this.Popularite, dos);

			} catch (IOException e) {
				throw new RuntimeException(e);
			}

		}

		public void writeData(org.jboss.marshalling.Marshaller dos) {
			try {

				// String

				writeString(this.Marque, dos);

				// String

				writeString(this.Type, dos);

				// String

				writeString(this.Couleur, dos);

				// String

				writeString(this.Taille, dos);

				// String

				writeString(this.Matiere, dos);

				// Float

				if (this.Prix_origine == null) {
					dos.writeByte(-1);
				} else {
					dos.writeByte(0);
					dos.writeFloat(this.Prix_origine);
				}

				// String

				writeString(this.Genre, dos);

				// String

				writeString(this.Saison, dos);

				// String

				writeString(this.Collection, dos);

				// String

				writeString(this.Popularite, dos);

			} catch (IOException e) {
				throw new RuntimeException(e);
			}

		}

		public String toString() {

			StringBuilder sb = new StringBuilder();
			sb.append(super.toString());
			sb.append("[");
			sb.append("Marque=" + Marque);
			sb.append(",Type=" + Type);
			sb.append(",Couleur=" + Couleur);
			sb.append(",Taille=" + Taille);
			sb.append(",Matiere=" + Matiere);
			sb.append(",Prix_origine=" + String.valueOf(Prix_origine));
			sb.append(",Genre=" + Genre);
			sb.append(",Saison=" + Saison);
			sb.append(",Collection=" + Collection);
			sb.append(",Popularite=" + Popularite);
			sb.append("]");

			return sb.toString();
		}

		/**
		 * Compare keys
		 */
		public int compareTo(out3Struct other) {

			int returnValue = -1;

			return returnValue;
		}

		private int checkNullsAndCompare(Object object1, Object object2) {
			int returnValue = 0;
			if (object1 instanceof Comparable && object2 instanceof Comparable) {
				returnValue = ((Comparable) object1).compareTo(object2);
			} else if (object1 != null && object2 != null) {
				returnValue = compareStrings(object1.toString(), object2.toString());
			} else if (object1 == null && object2 != null) {
				returnValue = 1;
			} else if (object1 != null && object2 == null) {
				returnValue = -1;
			} else {
				returnValue = 0;
			}

			return returnValue;
		}

		private int compareStrings(String string1, String string2) {
			return string1.compareTo(string2);
		}

	}

	public static class row3Struct implements routines.system.IPersistableRow<row3Struct> {
		final static byte[] commonByteArrayLock_RESTYLE_UP_unite_files = new byte[0];
		static byte[] commonByteArray_RESTYLE_UP_unite_files = new byte[0];

		public String Marque;

		public String getMarque() {
			return this.Marque;
		}

		public String Type;

		public String getType() {
			return this.Type;
		}

		public String Couleur;

		public String getCouleur() {
			return this.Couleur;
		}

		public String Taille;

		public String getTaille() {
			return this.Taille;
		}

		public String Matiere;

		public String getMatiere() {
			return this.Matiere;
		}

		public Float Prix_origine;

		public Float getPrix_origine() {
			return this.Prix_origine;
		}

		public String Genre;

		public String getGenre() {
			return this.Genre;
		}

		public String Saison;

		public String getSaison() {
			return this.Saison;
		}

		public String Collection;

		public String getCollection() {
			return this.Collection;
		}

		public String Popularite;

		public String getPopularite() {
			return this.Popularite;
		}

		private String readString(ObjectInputStream dis) throws IOException {
			String strReturn = null;
			int length = 0;
			length = dis.readInt();
			if (length == -1) {
				strReturn = null;
			} else {
				if (length > commonByteArray_RESTYLE_UP_unite_files.length) {
					if (length < 1024 && commonByteArray_RESTYLE_UP_unite_files.length == 0) {
						commonByteArray_RESTYLE_UP_unite_files = new byte[1024];
					} else {
						commonByteArray_RESTYLE_UP_unite_files = new byte[2 * length];
					}
				}
				dis.readFully(commonByteArray_RESTYLE_UP_unite_files, 0, length);
				strReturn = new String(commonByteArray_RESTYLE_UP_unite_files, 0, length, utf8Charset);
			}
			return strReturn;
		}

		private String readString(org.jboss.marshalling.Unmarshaller unmarshaller) throws IOException {
			String strReturn = null;
			int length = 0;
			length = unmarshaller.readInt();
			if (length == -1) {
				strReturn = null;
			} else {
				if (length > commonByteArray_RESTYLE_UP_unite_files.length) {
					if (length < 1024 && commonByteArray_RESTYLE_UP_unite_files.length == 0) {
						commonByteArray_RESTYLE_UP_unite_files = new byte[1024];
					} else {
						commonByteArray_RESTYLE_UP_unite_files = new byte[2 * length];
					}
				}
				unmarshaller.readFully(commonByteArray_RESTYLE_UP_unite_files, 0, length);
				strReturn = new String(commonByteArray_RESTYLE_UP_unite_files, 0, length, utf8Charset);
			}
			return strReturn;
		}

		private void writeString(String str, ObjectOutputStream dos) throws IOException {
			if (str == null) {
				dos.writeInt(-1);
			} else {
				byte[] byteArray = str.getBytes(utf8Charset);
				dos.writeInt(byteArray.length);
				dos.write(byteArray);
			}
		}

		private void writeString(String str, org.jboss.marshalling.Marshaller marshaller) throws IOException {
			if (str == null) {
				marshaller.writeInt(-1);
			} else {
				byte[] byteArray = str.getBytes(utf8Charset);
				marshaller.writeInt(byteArray.length);
				marshaller.write(byteArray);
			}
		}

		public void readData(ObjectInputStream dis) {

			synchronized (commonByteArrayLock_RESTYLE_UP_unite_files) {

				try {

					int length = 0;

					this.Marque = readString(dis);

					this.Type = readString(dis);

					this.Couleur = readString(dis);

					this.Taille = readString(dis);

					this.Matiere = readString(dis);

					length = dis.readByte();
					if (length == -1) {
						this.Prix_origine = null;
					} else {
						this.Prix_origine = dis.readFloat();
					}

					this.Genre = readString(dis);

					this.Saison = readString(dis);

					this.Collection = readString(dis);

					this.Popularite = readString(dis);

				} catch (IOException e) {
					throw new RuntimeException(e);

				}

			}

		}

		public void readData(org.jboss.marshalling.Unmarshaller dis) {

			synchronized (commonByteArrayLock_RESTYLE_UP_unite_files) {

				try {

					int length = 0;

					this.Marque = readString(dis);

					this.Type = readString(dis);

					this.Couleur = readString(dis);

					this.Taille = readString(dis);

					this.Matiere = readString(dis);

					length = dis.readByte();
					if (length == -1) {
						this.Prix_origine = null;
					} else {
						this.Prix_origine = dis.readFloat();
					}

					this.Genre = readString(dis);

					this.Saison = readString(dis);

					this.Collection = readString(dis);

					this.Popularite = readString(dis);

				} catch (IOException e) {
					throw new RuntimeException(e);

				}

			}

		}

		public void writeData(ObjectOutputStream dos) {
			try {

				// String

				writeString(this.Marque, dos);

				// String

				writeString(this.Type, dos);

				// String

				writeString(this.Couleur, dos);

				// String

				writeString(this.Taille, dos);

				// String

				writeString(this.Matiere, dos);

				// Float

				if (this.Prix_origine == null) {
					dos.writeByte(-1);
				} else {
					dos.writeByte(0);
					dos.writeFloat(this.Prix_origine);
				}

				// String

				writeString(this.Genre, dos);

				// String

				writeString(this.Saison, dos);

				// String

				writeString(this.Collection, dos);

				// String

				writeString(this.Popularite, dos);

			} catch (IOException e) {
				throw new RuntimeException(e);
			}

		}

		public void writeData(org.jboss.marshalling.Marshaller dos) {
			try {

				// String

				writeString(this.Marque, dos);

				// String

				writeString(this.Type, dos);

				// String

				writeString(this.Couleur, dos);

				// String

				writeString(this.Taille, dos);

				// String

				writeString(this.Matiere, dos);

				// Float

				if (this.Prix_origine == null) {
					dos.writeByte(-1);
				} else {
					dos.writeByte(0);
					dos.writeFloat(this.Prix_origine);
				}

				// String

				writeString(this.Genre, dos);

				// String

				writeString(this.Saison, dos);

				// String

				writeString(this.Collection, dos);

				// String

				writeString(this.Popularite, dos);

			} catch (IOException e) {
				throw new RuntimeException(e);
			}

		}

		public String toString() {

			StringBuilder sb = new StringBuilder();
			sb.append(super.toString());
			sb.append("[");
			sb.append("Marque=" + Marque);
			sb.append(",Type=" + Type);
			sb.append(",Couleur=" + Couleur);
			sb.append(",Taille=" + Taille);
			sb.append(",Matiere=" + Matiere);
			sb.append(",Prix_origine=" + String.valueOf(Prix_origine));
			sb.append(",Genre=" + Genre);
			sb.append(",Saison=" + Saison);
			sb.append(",Collection=" + Collection);
			sb.append(",Popularite=" + Popularite);
			sb.append("]");

			return sb.toString();
		}

		/**
		 * Compare keys
		 */
		public int compareTo(row3Struct other) {

			int returnValue = -1;

			return returnValue;
		}

		private int checkNullsAndCompare(Object object1, Object object2) {
			int returnValue = 0;
			if (object1 instanceof Comparable && object2 instanceof Comparable) {
				returnValue = ((Comparable) object1).compareTo(object2);
			} else if (object1 != null && object2 != null) {
				returnValue = compareStrings(object1.toString(), object2.toString());
			} else if (object1 == null && object2 != null) {
				returnValue = 1;
			} else if (object1 != null && object2 == null) {
				returnValue = -1;
			} else {
				returnValue = 0;
			}

			return returnValue;
		}

		private int compareStrings(String string1, String string2) {
			return string1.compareTo(string2);
		}

	}

	public static class out1Struct implements routines.system.IPersistableRow<out1Struct> {
		final static byte[] commonByteArrayLock_RESTYLE_UP_unite_files = new byte[0];
		static byte[] commonByteArray_RESTYLE_UP_unite_files = new byte[0];

		public String Marque;

		public String getMarque() {
			return this.Marque;
		}

		public String Type;

		public String getType() {
			return this.Type;
		}

		public String Couleur;

		public String getCouleur() {
			return this.Couleur;
		}

		public String Taille;

		public String getTaille() {
			return this.Taille;
		}

		public String Matiere;

		public String getMatiere() {
			return this.Matiere;
		}

		public Float Prix_origine;

		public Float getPrix_origine() {
			return this.Prix_origine;
		}

		public String Genre;

		public String getGenre() {
			return this.Genre;
		}

		public String Saison;

		public String getSaison() {
			return this.Saison;
		}

		public String Collection;

		public String getCollection() {
			return this.Collection;
		}

		public String Popularite;

		public String getPopularite() {
			return this.Popularite;
		}

		private String readString(ObjectInputStream dis) throws IOException {
			String strReturn = null;
			int length = 0;
			length = dis.readInt();
			if (length == -1) {
				strReturn = null;
			} else {
				if (length > commonByteArray_RESTYLE_UP_unite_files.length) {
					if (length < 1024 && commonByteArray_RESTYLE_UP_unite_files.length == 0) {
						commonByteArray_RESTYLE_UP_unite_files = new byte[1024];
					} else {
						commonByteArray_RESTYLE_UP_unite_files = new byte[2 * length];
					}
				}
				dis.readFully(commonByteArray_RESTYLE_UP_unite_files, 0, length);
				strReturn = new String(commonByteArray_RESTYLE_UP_unite_files, 0, length, utf8Charset);
			}
			return strReturn;
		}

		private String readString(org.jboss.marshalling.Unmarshaller unmarshaller) throws IOException {
			String strReturn = null;
			int length = 0;
			length = unmarshaller.readInt();
			if (length == -1) {
				strReturn = null;
			} else {
				if (length > commonByteArray_RESTYLE_UP_unite_files.length) {
					if (length < 1024 && commonByteArray_RESTYLE_UP_unite_files.length == 0) {
						commonByteArray_RESTYLE_UP_unite_files = new byte[1024];
					} else {
						commonByteArray_RESTYLE_UP_unite_files = new byte[2 * length];
					}
				}
				unmarshaller.readFully(commonByteArray_RESTYLE_UP_unite_files, 0, length);
				strReturn = new String(commonByteArray_RESTYLE_UP_unite_files, 0, length, utf8Charset);
			}
			return strReturn;
		}

		private void writeString(String str, ObjectOutputStream dos) throws IOException {
			if (str == null) {
				dos.writeInt(-1);
			} else {
				byte[] byteArray = str.getBytes(utf8Charset);
				dos.writeInt(byteArray.length);
				dos.write(byteArray);
			}
		}

		private void writeString(String str, org.jboss.marshalling.Marshaller marshaller) throws IOException {
			if (str == null) {
				marshaller.writeInt(-1);
			} else {
				byte[] byteArray = str.getBytes(utf8Charset);
				marshaller.writeInt(byteArray.length);
				marshaller.write(byteArray);
			}
		}

		public void readData(ObjectInputStream dis) {

			synchronized (commonByteArrayLock_RESTYLE_UP_unite_files) {

				try {

					int length = 0;

					this.Marque = readString(dis);

					this.Type = readString(dis);

					this.Couleur = readString(dis);

					this.Taille = readString(dis);

					this.Matiere = readString(dis);

					length = dis.readByte();
					if (length == -1) {
						this.Prix_origine = null;
					} else {
						this.Prix_origine = dis.readFloat();
					}

					this.Genre = readString(dis);

					this.Saison = readString(dis);

					this.Collection = readString(dis);

					this.Popularite = readString(dis);

				} catch (IOException e) {
					throw new RuntimeException(e);

				}

			}

		}

		public void readData(org.jboss.marshalling.Unmarshaller dis) {

			synchronized (commonByteArrayLock_RESTYLE_UP_unite_files) {

				try {

					int length = 0;

					this.Marque = readString(dis);

					this.Type = readString(dis);

					this.Couleur = readString(dis);

					this.Taille = readString(dis);

					this.Matiere = readString(dis);

					length = dis.readByte();
					if (length == -1) {
						this.Prix_origine = null;
					} else {
						this.Prix_origine = dis.readFloat();
					}

					this.Genre = readString(dis);

					this.Saison = readString(dis);

					this.Collection = readString(dis);

					this.Popularite = readString(dis);

				} catch (IOException e) {
					throw new RuntimeException(e);

				}

			}

		}

		public void writeData(ObjectOutputStream dos) {
			try {

				// String

				writeString(this.Marque, dos);

				// String

				writeString(this.Type, dos);

				// String

				writeString(this.Couleur, dos);

				// String

				writeString(this.Taille, dos);

				// String

				writeString(this.Matiere, dos);

				// Float

				if (this.Prix_origine == null) {
					dos.writeByte(-1);
				} else {
					dos.writeByte(0);
					dos.writeFloat(this.Prix_origine);
				}

				// String

				writeString(this.Genre, dos);

				// String

				writeString(this.Saison, dos);

				// String

				writeString(this.Collection, dos);

				// String

				writeString(this.Popularite, dos);

			} catch (IOException e) {
				throw new RuntimeException(e);
			}

		}

		public void writeData(org.jboss.marshalling.Marshaller dos) {
			try {

				// String

				writeString(this.Marque, dos);

				// String

				writeString(this.Type, dos);

				// String

				writeString(this.Couleur, dos);

				// String

				writeString(this.Taille, dos);

				// String

				writeString(this.Matiere, dos);

				// Float

				if (this.Prix_origine == null) {
					dos.writeByte(-1);
				} else {
					dos.writeByte(0);
					dos.writeFloat(this.Prix_origine);
				}

				// String

				writeString(this.Genre, dos);

				// String

				writeString(this.Saison, dos);

				// String

				writeString(this.Collection, dos);

				// String

				writeString(this.Popularite, dos);

			} catch (IOException e) {
				throw new RuntimeException(e);
			}

		}

		public String toString() {

			StringBuilder sb = new StringBuilder();
			sb.append(super.toString());
			sb.append("[");
			sb.append("Marque=" + Marque);
			sb.append(",Type=" + Type);
			sb.append(",Couleur=" + Couleur);
			sb.append(",Taille=" + Taille);
			sb.append(",Matiere=" + Matiere);
			sb.append(",Prix_origine=" + String.valueOf(Prix_origine));
			sb.append(",Genre=" + Genre);
			sb.append(",Saison=" + Saison);
			sb.append(",Collection=" + Collection);
			sb.append(",Popularite=" + Popularite);
			sb.append("]");

			return sb.toString();
		}

		/**
		 * Compare keys
		 */
		public int compareTo(out1Struct other) {

			int returnValue = -1;

			return returnValue;
		}

		private int checkNullsAndCompare(Object object1, Object object2) {
			int returnValue = 0;
			if (object1 instanceof Comparable && object2 instanceof Comparable) {
				returnValue = ((Comparable) object1).compareTo(object2);
			} else if (object1 != null && object2 != null) {
				returnValue = compareStrings(object1.toString(), object2.toString());
			} else if (object1 == null && object2 != null) {
				returnValue = 1;
			} else if (object1 != null && object2 == null) {
				returnValue = -1;
			} else {
				returnValue = 0;
			}

			return returnValue;
		}

		private int compareStrings(String string1, String string2) {
			return string1.compareTo(string2);
		}

	}

	public static class row1Struct implements routines.system.IPersistableRow<row1Struct> {
		final static byte[] commonByteArrayLock_RESTYLE_UP_unite_files = new byte[0];
		static byte[] commonByteArray_RESTYLE_UP_unite_files = new byte[0];

		public String Type;

		public String getType() {
			return this.Type;
		}

		public String Marque;

		public String getMarque() {
			return this.Marque;
		}

		public String Genre;

		public String getGenre() {
			return this.Genre;
		}

		public String Matiere;

		public String getMatiere() {
			return this.Matiere;
		}

		public String Taille;

		public String getTaille() {
			return this.Taille;
		}

		public String Saison;

		public String getSaison() {
			return this.Saison;
		}

		public String Couleur;

		public String getCouleur() {
			return this.Couleur;
		}

		public String Collection;

		public String getCollection() {
			return this.Collection;
		}

		public String Popularite;

		public String getPopularite() {
			return this.Popularite;
		}

		public Float Prix_origine;

		public Float getPrix_origine() {
			return this.Prix_origine;
		}

		private String readString(ObjectInputStream dis) throws IOException {
			String strReturn = null;
			int length = 0;
			length = dis.readInt();
			if (length == -1) {
				strReturn = null;
			} else {
				if (length > commonByteArray_RESTYLE_UP_unite_files.length) {
					if (length < 1024 && commonByteArray_RESTYLE_UP_unite_files.length == 0) {
						commonByteArray_RESTYLE_UP_unite_files = new byte[1024];
					} else {
						commonByteArray_RESTYLE_UP_unite_files = new byte[2 * length];
					}
				}
				dis.readFully(commonByteArray_RESTYLE_UP_unite_files, 0, length);
				strReturn = new String(commonByteArray_RESTYLE_UP_unite_files, 0, length, utf8Charset);
			}
			return strReturn;
		}

		private String readString(org.jboss.marshalling.Unmarshaller unmarshaller) throws IOException {
			String strReturn = null;
			int length = 0;
			length = unmarshaller.readInt();
			if (length == -1) {
				strReturn = null;
			} else {
				if (length > commonByteArray_RESTYLE_UP_unite_files.length) {
					if (length < 1024 && commonByteArray_RESTYLE_UP_unite_files.length == 0) {
						commonByteArray_RESTYLE_UP_unite_files = new byte[1024];
					} else {
						commonByteArray_RESTYLE_UP_unite_files = new byte[2 * length];
					}
				}
				unmarshaller.readFully(commonByteArray_RESTYLE_UP_unite_files, 0, length);
				strReturn = new String(commonByteArray_RESTYLE_UP_unite_files, 0, length, utf8Charset);
			}
			return strReturn;
		}

		private void writeString(String str, ObjectOutputStream dos) throws IOException {
			if (str == null) {
				dos.writeInt(-1);
			} else {
				byte[] byteArray = str.getBytes(utf8Charset);
				dos.writeInt(byteArray.length);
				dos.write(byteArray);
			}
		}

		private void writeString(String str, org.jboss.marshalling.Marshaller marshaller) throws IOException {
			if (str == null) {
				marshaller.writeInt(-1);
			} else {
				byte[] byteArray = str.getBytes(utf8Charset);
				marshaller.writeInt(byteArray.length);
				marshaller.write(byteArray);
			}
		}

		public void readData(ObjectInputStream dis) {

			synchronized (commonByteArrayLock_RESTYLE_UP_unite_files) {

				try {

					int length = 0;

					this.Type = readString(dis);

					this.Marque = readString(dis);

					this.Genre = readString(dis);

					this.Matiere = readString(dis);

					this.Taille = readString(dis);

					this.Saison = readString(dis);

					this.Couleur = readString(dis);

					this.Collection = readString(dis);

					this.Popularite = readString(dis);

					length = dis.readByte();
					if (length == -1) {
						this.Prix_origine = null;
					} else {
						this.Prix_origine = dis.readFloat();
					}

				} catch (IOException e) {
					throw new RuntimeException(e);

				}

			}

		}

		public void readData(org.jboss.marshalling.Unmarshaller dis) {

			synchronized (commonByteArrayLock_RESTYLE_UP_unite_files) {

				try {

					int length = 0;

					this.Type = readString(dis);

					this.Marque = readString(dis);

					this.Genre = readString(dis);

					this.Matiere = readString(dis);

					this.Taille = readString(dis);

					this.Saison = readString(dis);

					this.Couleur = readString(dis);

					this.Collection = readString(dis);

					this.Popularite = readString(dis);

					length = dis.readByte();
					if (length == -1) {
						this.Prix_origine = null;
					} else {
						this.Prix_origine = dis.readFloat();
					}

				} catch (IOException e) {
					throw new RuntimeException(e);

				}

			}

		}

		public void writeData(ObjectOutputStream dos) {
			try {

				// String

				writeString(this.Type, dos);

				// String

				writeString(this.Marque, dos);

				// String

				writeString(this.Genre, dos);

				// String

				writeString(this.Matiere, dos);

				// String

				writeString(this.Taille, dos);

				// String

				writeString(this.Saison, dos);

				// String

				writeString(this.Couleur, dos);

				// String

				writeString(this.Collection, dos);

				// String

				writeString(this.Popularite, dos);

				// Float

				if (this.Prix_origine == null) {
					dos.writeByte(-1);
				} else {
					dos.writeByte(0);
					dos.writeFloat(this.Prix_origine);
				}

			} catch (IOException e) {
				throw new RuntimeException(e);
			}

		}

		public void writeData(org.jboss.marshalling.Marshaller dos) {
			try {

				// String

				writeString(this.Type, dos);

				// String

				writeString(this.Marque, dos);

				// String

				writeString(this.Genre, dos);

				// String

				writeString(this.Matiere, dos);

				// String

				writeString(this.Taille, dos);

				// String

				writeString(this.Saison, dos);

				// String

				writeString(this.Couleur, dos);

				// String

				writeString(this.Collection, dos);

				// String

				writeString(this.Popularite, dos);

				// Float

				if (this.Prix_origine == null) {
					dos.writeByte(-1);
				} else {
					dos.writeByte(0);
					dos.writeFloat(this.Prix_origine);
				}

			} catch (IOException e) {
				throw new RuntimeException(e);
			}

		}

		public String toString() {

			StringBuilder sb = new StringBuilder();
			sb.append(super.toString());
			sb.append("[");
			sb.append("Type=" + Type);
			sb.append(",Marque=" + Marque);
			sb.append(",Genre=" + Genre);
			sb.append(",Matiere=" + Matiere);
			sb.append(",Taille=" + Taille);
			sb.append(",Saison=" + Saison);
			sb.append(",Couleur=" + Couleur);
			sb.append(",Collection=" + Collection);
			sb.append(",Popularite=" + Popularite);
			sb.append(",Prix_origine=" + String.valueOf(Prix_origine));
			sb.append("]");

			return sb.toString();
		}

		/**
		 * Compare keys
		 */
		public int compareTo(row1Struct other) {

			int returnValue = -1;

			return returnValue;
		}

		private int checkNullsAndCompare(Object object1, Object object2) {
			int returnValue = 0;
			if (object1 instanceof Comparable && object2 instanceof Comparable) {
				returnValue = ((Comparable) object1).compareTo(object2);
			} else if (object1 != null && object2 != null) {
				returnValue = compareStrings(object1.toString(), object2.toString());
			} else if (object1 == null && object2 != null) {
				returnValue = 1;
			} else if (object1 != null && object2 == null) {
				returnValue = -1;
			} else {
				returnValue = 0;
			}

			return returnValue;
		}

		private int compareStrings(String string1, String string2) {
			return string1.compareTo(string2);
		}

	}

	public static class out2Struct implements routines.system.IPersistableRow<out2Struct> {
		final static byte[] commonByteArrayLock_RESTYLE_UP_unite_files = new byte[0];
		static byte[] commonByteArray_RESTYLE_UP_unite_files = new byte[0];

		public String Brand;

		public String getBrand() {
			return this.Brand;
		}

		public String Category;

		public String getCategory() {
			return this.Category;
		}

		public String Color;

		public String getColor() {
			return this.Color;
		}

		public String Size;

		public String getSize() {
			return this.Size;
		}

		public String Material;

		public String getMaterial() {
			return this.Material;
		}

		public Float Price;

		public Float getPrice() {
			return this.Price;
		}

		private String readString(ObjectInputStream dis) throws IOException {
			String strReturn = null;
			int length = 0;
			length = dis.readInt();
			if (length == -1) {
				strReturn = null;
			} else {
				if (length > commonByteArray_RESTYLE_UP_unite_files.length) {
					if (length < 1024 && commonByteArray_RESTYLE_UP_unite_files.length == 0) {
						commonByteArray_RESTYLE_UP_unite_files = new byte[1024];
					} else {
						commonByteArray_RESTYLE_UP_unite_files = new byte[2 * length];
					}
				}
				dis.readFully(commonByteArray_RESTYLE_UP_unite_files, 0, length);
				strReturn = new String(commonByteArray_RESTYLE_UP_unite_files, 0, length, utf8Charset);
			}
			return strReturn;
		}

		private String readString(org.jboss.marshalling.Unmarshaller unmarshaller) throws IOException {
			String strReturn = null;
			int length = 0;
			length = unmarshaller.readInt();
			if (length == -1) {
				strReturn = null;
			} else {
				if (length > commonByteArray_RESTYLE_UP_unite_files.length) {
					if (length < 1024 && commonByteArray_RESTYLE_UP_unite_files.length == 0) {
						commonByteArray_RESTYLE_UP_unite_files = new byte[1024];
					} else {
						commonByteArray_RESTYLE_UP_unite_files = new byte[2 * length];
					}
				}
				unmarshaller.readFully(commonByteArray_RESTYLE_UP_unite_files, 0, length);
				strReturn = new String(commonByteArray_RESTYLE_UP_unite_files, 0, length, utf8Charset);
			}
			return strReturn;
		}

		private void writeString(String str, ObjectOutputStream dos) throws IOException {
			if (str == null) {
				dos.writeInt(-1);
			} else {
				byte[] byteArray = str.getBytes(utf8Charset);
				dos.writeInt(byteArray.length);
				dos.write(byteArray);
			}
		}

		private void writeString(String str, org.jboss.marshalling.Marshaller marshaller) throws IOException {
			if (str == null) {
				marshaller.writeInt(-1);
			} else {
				byte[] byteArray = str.getBytes(utf8Charset);
				marshaller.writeInt(byteArray.length);
				marshaller.write(byteArray);
			}
		}

		public void readData(ObjectInputStream dis) {

			synchronized (commonByteArrayLock_RESTYLE_UP_unite_files) {

				try {

					int length = 0;

					this.Brand = readString(dis);

					this.Category = readString(dis);

					this.Color = readString(dis);

					this.Size = readString(dis);

					this.Material = readString(dis);

					length = dis.readByte();
					if (length == -1) {
						this.Price = null;
					} else {
						this.Price = dis.readFloat();
					}

				} catch (IOException e) {
					throw new RuntimeException(e);

				}

			}

		}

		public void readData(org.jboss.marshalling.Unmarshaller dis) {

			synchronized (commonByteArrayLock_RESTYLE_UP_unite_files) {

				try {

					int length = 0;

					this.Brand = readString(dis);

					this.Category = readString(dis);

					this.Color = readString(dis);

					this.Size = readString(dis);

					this.Material = readString(dis);

					length = dis.readByte();
					if (length == -1) {
						this.Price = null;
					} else {
						this.Price = dis.readFloat();
					}

				} catch (IOException e) {
					throw new RuntimeException(e);

				}

			}

		}

		public void writeData(ObjectOutputStream dos) {
			try {

				// String

				writeString(this.Brand, dos);

				// String

				writeString(this.Category, dos);

				// String

				writeString(this.Color, dos);

				// String

				writeString(this.Size, dos);

				// String

				writeString(this.Material, dos);

				// Float

				if (this.Price == null) {
					dos.writeByte(-1);
				} else {
					dos.writeByte(0);
					dos.writeFloat(this.Price);
				}

			} catch (IOException e) {
				throw new RuntimeException(e);
			}

		}

		public void writeData(org.jboss.marshalling.Marshaller dos) {
			try {

				// String

				writeString(this.Brand, dos);

				// String

				writeString(this.Category, dos);

				// String

				writeString(this.Color, dos);

				// String

				writeString(this.Size, dos);

				// String

				writeString(this.Material, dos);

				// Float

				if (this.Price == null) {
					dos.writeByte(-1);
				} else {
					dos.writeByte(0);
					dos.writeFloat(this.Price);
				}

			} catch (IOException e) {
				throw new RuntimeException(e);
			}

		}

		public String toString() {

			StringBuilder sb = new StringBuilder();
			sb.append(super.toString());
			sb.append("[");
			sb.append("Brand=" + Brand);
			sb.append(",Category=" + Category);
			sb.append(",Color=" + Color);
			sb.append(",Size=" + Size);
			sb.append(",Material=" + Material);
			sb.append(",Price=" + String.valueOf(Price));
			sb.append("]");

			return sb.toString();
		}

		/**
		 * Compare keys
		 */
		public int compareTo(out2Struct other) {

			int returnValue = -1;

			return returnValue;
		}

		private int checkNullsAndCompare(Object object1, Object object2) {
			int returnValue = 0;
			if (object1 instanceof Comparable && object2 instanceof Comparable) {
				returnValue = ((Comparable) object1).compareTo(object2);
			} else if (object1 != null && object2 != null) {
				returnValue = compareStrings(object1.toString(), object2.toString());
			} else if (object1 == null && object2 != null) {
				returnValue = 1;
			} else if (object1 != null && object2 == null) {
				returnValue = -1;
			} else {
				returnValue = 0;
			}

			return returnValue;
		}

		private int compareStrings(String string1, String string2) {
			return string1.compareTo(string2);
		}

	}

	public static class row2Struct implements routines.system.IPersistableRow<row2Struct> {
		final static byte[] commonByteArrayLock_RESTYLE_UP_unite_files = new byte[0];
		static byte[] commonByteArray_RESTYLE_UP_unite_files = new byte[0];

		public String Brand;

		public String getBrand() {
			return this.Brand;
		}

		public String Category;

		public String getCategory() {
			return this.Category;
		}

		public String Color;

		public String getColor() {
			return this.Color;
		}

		public String Size;

		public String getSize() {
			return this.Size;
		}

		public String Material;

		public String getMaterial() {
			return this.Material;
		}

		public Integer Price;

		public Integer getPrice() {
			return this.Price;
		}

		private String readString(ObjectInputStream dis) throws IOException {
			String strReturn = null;
			int length = 0;
			length = dis.readInt();
			if (length == -1) {
				strReturn = null;
			} else {
				if (length > commonByteArray_RESTYLE_UP_unite_files.length) {
					if (length < 1024 && commonByteArray_RESTYLE_UP_unite_files.length == 0) {
						commonByteArray_RESTYLE_UP_unite_files = new byte[1024];
					} else {
						commonByteArray_RESTYLE_UP_unite_files = new byte[2 * length];
					}
				}
				dis.readFully(commonByteArray_RESTYLE_UP_unite_files, 0, length);
				strReturn = new String(commonByteArray_RESTYLE_UP_unite_files, 0, length, utf8Charset);
			}
			return strReturn;
		}

		private String readString(org.jboss.marshalling.Unmarshaller unmarshaller) throws IOException {
			String strReturn = null;
			int length = 0;
			length = unmarshaller.readInt();
			if (length == -1) {
				strReturn = null;
			} else {
				if (length > commonByteArray_RESTYLE_UP_unite_files.length) {
					if (length < 1024 && commonByteArray_RESTYLE_UP_unite_files.length == 0) {
						commonByteArray_RESTYLE_UP_unite_files = new byte[1024];
					} else {
						commonByteArray_RESTYLE_UP_unite_files = new byte[2 * length];
					}
				}
				unmarshaller.readFully(commonByteArray_RESTYLE_UP_unite_files, 0, length);
				strReturn = new String(commonByteArray_RESTYLE_UP_unite_files, 0, length, utf8Charset);
			}
			return strReturn;
		}

		private void writeString(String str, ObjectOutputStream dos) throws IOException {
			if (str == null) {
				dos.writeInt(-1);
			} else {
				byte[] byteArray = str.getBytes(utf8Charset);
				dos.writeInt(byteArray.length);
				dos.write(byteArray);
			}
		}

		private void writeString(String str, org.jboss.marshalling.Marshaller marshaller) throws IOException {
			if (str == null) {
				marshaller.writeInt(-1);
			} else {
				byte[] byteArray = str.getBytes(utf8Charset);
				marshaller.writeInt(byteArray.length);
				marshaller.write(byteArray);
			}
		}

		private Integer readInteger(ObjectInputStream dis) throws IOException {
			Integer intReturn;
			int length = 0;
			length = dis.readByte();
			if (length == -1) {
				intReturn = null;
			} else {
				intReturn = dis.readInt();
			}
			return intReturn;
		}

		private Integer readInteger(org.jboss.marshalling.Unmarshaller dis) throws IOException {
			Integer intReturn;
			int length = 0;
			length = dis.readByte();
			if (length == -1) {
				intReturn = null;
			} else {
				intReturn = dis.readInt();
			}
			return intReturn;
		}

		private void writeInteger(Integer intNum, ObjectOutputStream dos) throws IOException {
			if (intNum == null) {
				dos.writeByte(-1);
			} else {
				dos.writeByte(0);
				dos.writeInt(intNum);
			}
		}

		private void writeInteger(Integer intNum, org.jboss.marshalling.Marshaller marshaller) throws IOException {
			if (intNum == null) {
				marshaller.writeByte(-1);
			} else {
				marshaller.writeByte(0);
				marshaller.writeInt(intNum);
			}
		}

		public void readData(ObjectInputStream dis) {

			synchronized (commonByteArrayLock_RESTYLE_UP_unite_files) {

				try {

					int length = 0;

					this.Brand = readString(dis);

					this.Category = readString(dis);

					this.Color = readString(dis);

					this.Size = readString(dis);

					this.Material = readString(dis);

					this.Price = readInteger(dis);

				} catch (IOException e) {
					throw new RuntimeException(e);

				}

			}

		}

		public void readData(org.jboss.marshalling.Unmarshaller dis) {

			synchronized (commonByteArrayLock_RESTYLE_UP_unite_files) {

				try {

					int length = 0;

					this.Brand = readString(dis);

					this.Category = readString(dis);

					this.Color = readString(dis);

					this.Size = readString(dis);

					this.Material = readString(dis);

					this.Price = readInteger(dis);

				} catch (IOException e) {
					throw new RuntimeException(e);

				}

			}

		}

		public void writeData(ObjectOutputStream dos) {
			try {

				// String

				writeString(this.Brand, dos);

				// String

				writeString(this.Category, dos);

				// String

				writeString(this.Color, dos);

				// String

				writeString(this.Size, dos);

				// String

				writeString(this.Material, dos);

				// Integer

				writeInteger(this.Price, dos);

			} catch (IOException e) {
				throw new RuntimeException(e);
			}

		}

		public void writeData(org.jboss.marshalling.Marshaller dos) {
			try {

				// String

				writeString(this.Brand, dos);

				// String

				writeString(this.Category, dos);

				// String

				writeString(this.Color, dos);

				// String

				writeString(this.Size, dos);

				// String

				writeString(this.Material, dos);

				// Integer

				writeInteger(this.Price, dos);

			} catch (IOException e) {
				throw new RuntimeException(e);
			}

		}

		public String toString() {

			StringBuilder sb = new StringBuilder();
			sb.append(super.toString());
			sb.append("[");
			sb.append("Brand=" + Brand);
			sb.append(",Category=" + Category);
			sb.append(",Color=" + Color);
			sb.append(",Size=" + Size);
			sb.append(",Material=" + Material);
			sb.append(",Price=" + String.valueOf(Price));
			sb.append("]");

			return sb.toString();
		}

		/**
		 * Compare keys
		 */
		public int compareTo(row2Struct other) {

			int returnValue = -1;

			return returnValue;
		}

		private int checkNullsAndCompare(Object object1, Object object2) {
			int returnValue = 0;
			if (object1 instanceof Comparable && object2 instanceof Comparable) {
				returnValue = ((Comparable) object1).compareTo(object2);
			} else if (object1 != null && object2 != null) {
				returnValue = compareStrings(object1.toString(), object2.toString());
			} else if (object1 == null && object2 != null) {
				returnValue = 1;
			} else if (object1 != null && object2 == null) {
				returnValue = -1;
			} else {
				returnValue = 0;
			}

			return returnValue;
		}

		private int compareStrings(String string1, String string2) {
			return string1.compareTo(string2);
		}

	}

	public void tFileInputDelimited_1Process(final java.util.Map<String, Object> globalMap) throws TalendException {
		globalMap.put("tFileInputDelimited_1_SUBPROCESS_STATE", 0);

		final boolean execStat = this.execStat;

		String iterateId = "";

		String currentComponent = "";
		java.util.Map<String, Object> resourceMap = new java.util.HashMap<String, Object>();

		try {
			// TDI-39566 avoid throwing an useless Exception
			boolean resumeIt = true;
			if (globalResumeTicket == false && resumeEntryMethodName != null) {
				String currentMethodName = new java.lang.Exception().getStackTrace()[0].getMethodName();
				resumeIt = resumeEntryMethodName.equals(currentMethodName);
			}
			if (resumeIt || globalResumeTicket) { // start the resume
				globalResumeTicket = true;

				row1Struct row1 = new row1Struct();
				out1Struct out1 = new out1Struct();

				row2Struct row2 = new row2Struct();
				out2Struct out2 = new out2Struct();

				row3Struct row3 = new row3Struct();
				out3Struct out3 = new out3Struct();

				/**
				 * [tDBOutput_1 begin ] start
				 */

				ok_Hash.put("tDBOutput_1", false);
				start_Hash.put("tDBOutput_1", System.currentTimeMillis());

				currentComponent = "tDBOutput_1";

				if (execStat) {
					runStat.updateStatOnConnection(resourceMap, iterateId, 0, 0, "out3");
				}

				int tos_count_tDBOutput_1 = 0;

				String dbschema_tDBOutput_1 = null;
				dbschema_tDBOutput_1 = "";

				String tableName_tDBOutput_1 = null;
				if (dbschema_tDBOutput_1 == null || dbschema_tDBOutput_1.trim().length() == 0) {
					tableName_tDBOutput_1 = ("Restyleup");
				} else {
					tableName_tDBOutput_1 = dbschema_tDBOutput_1 + "\".\"" + ("Restyleup");
				}

				int nb_line_tDBOutput_1 = 0;
				int nb_line_update_tDBOutput_1 = 0;
				int nb_line_inserted_tDBOutput_1 = 0;
				int nb_line_deleted_tDBOutput_1 = 0;
				int nb_line_rejected_tDBOutput_1 = 0;

				int deletedCount_tDBOutput_1 = 0;
				int updatedCount_tDBOutput_1 = 0;
				int insertedCount_tDBOutput_1 = 0;
				int rowsToCommitCount_tDBOutput_1 = 0;
				int rejectedCount_tDBOutput_1 = 0;

				boolean whetherReject_tDBOutput_1 = false;

				java.sql.Connection conn_tDBOutput_1 = null;
				String dbUser_tDBOutput_1 = null;

				java.lang.Class.forName("org.postgresql.Driver");

				String url_tDBOutput_1 = "jdbc:postgresql://" + "localhost" + ":" + "5432" + "/" + "Restyleup";
				dbUser_tDBOutput_1 = "postgres";

				final String decryptedPassword_tDBOutput_1 = routines.system.PasswordEncryptUtil.decryptPassword(
						"enc:routine.encryption.key.v1:6CKZa+I0lVwMwJF/qJ3ISjPZHXP9LEWatl4LoxfOevyeaQ3H");

				String dbPwd_tDBOutput_1 = decryptedPassword_tDBOutput_1;

				conn_tDBOutput_1 = java.sql.DriverManager.getConnection(url_tDBOutput_1, dbUser_tDBOutput_1,
						dbPwd_tDBOutput_1);

				resourceMap.put("conn_tDBOutput_1", conn_tDBOutput_1);
				conn_tDBOutput_1.setAutoCommit(false);
				int commitEvery_tDBOutput_1 = 10000;
				int commitCounter_tDBOutput_1 = 0;

				int batchSize_tDBOutput_1 = 10000;
				int batchSizeCounter_tDBOutput_1 = 0;

				int count_tDBOutput_1 = 0;
				java.sql.DatabaseMetaData dbMetaData_tDBOutput_1 = conn_tDBOutput_1.getMetaData();
				boolean whetherExist_tDBOutput_1 = false;
				try (java.sql.ResultSet rsTable_tDBOutput_1 = dbMetaData_tDBOutput_1.getTables(null, null, null,
						new String[] { "TABLE" })) {
					String defaultSchema_tDBOutput_1 = "public";
					if (dbschema_tDBOutput_1 == null || dbschema_tDBOutput_1.trim().length() == 0) {
						try (java.sql.Statement stmtSchema_tDBOutput_1 = conn_tDBOutput_1.createStatement();
								java.sql.ResultSet rsSchema_tDBOutput_1 = stmtSchema_tDBOutput_1
										.executeQuery("select current_schema() ")) {
							while (rsSchema_tDBOutput_1.next()) {
								defaultSchema_tDBOutput_1 = rsSchema_tDBOutput_1.getString("current_schema");
							}
						}
					}
					while (rsTable_tDBOutput_1.next()) {
						String table_tDBOutput_1 = rsTable_tDBOutput_1.getString("TABLE_NAME");
						String schema_tDBOutput_1 = rsTable_tDBOutput_1.getString("TABLE_SCHEM");
						if (table_tDBOutput_1.equals(("Restyleup")) && (schema_tDBOutput_1.equals(dbschema_tDBOutput_1)
								|| ((dbschema_tDBOutput_1 == null || dbschema_tDBOutput_1.trim().length() == 0)
										&& defaultSchema_tDBOutput_1.equals(schema_tDBOutput_1)))) {
							whetherExist_tDBOutput_1 = true;
							break;
						}
					}
				}
				if (whetherExist_tDBOutput_1) {
					try (java.sql.Statement stmtDrop_tDBOutput_1 = conn_tDBOutput_1.createStatement()) {
						stmtDrop_tDBOutput_1.execute("DROP TABLE \"" + tableName_tDBOutput_1 + "\"");
					}
				}
				try (java.sql.Statement stmtCreate_tDBOutput_1 = conn_tDBOutput_1.createStatement()) {
					stmtCreate_tDBOutput_1.execute("CREATE TABLE \"" + tableName_tDBOutput_1
							+ "\"(\"Marque\" VARCHAR(255)  ,\"Type\" VARCHAR(255)  ,\"Couleur\" VARCHAR(255)  ,\"Taille\" VARCHAR(255)  ,\"Matiere\" VARCHAR(255)  ,\"Prix_origine\" FLOAT4 ,\"Genre\" VARCHAR(255)  ,\"Saison\" VARCHAR(255)  ,\"Collection\" VARCHAR(255)  ,\"Popularite\" VARCHAR(255)  )");
				}
				String insert_tDBOutput_1 = "INSERT INTO \"" + tableName_tDBOutput_1
						+ "\" (\"Marque\",\"Type\",\"Couleur\",\"Taille\",\"Matiere\",\"Prix_origine\",\"Genre\",\"Saison\",\"Collection\",\"Popularite\") VALUES (?,?,?,?,?,?,?,?,?,?)";

				java.sql.PreparedStatement pstmt_tDBOutput_1 = conn_tDBOutput_1.prepareStatement(insert_tDBOutput_1);
				resourceMap.put("pstmt_tDBOutput_1", pstmt_tDBOutput_1);

				/**
				 * [tDBOutput_1 begin ] stop
				 */

				/**
				 * [tMap_3 begin ] start
				 */

				ok_Hash.put("tMap_3", false);
				start_Hash.put("tMap_3", System.currentTimeMillis());

				currentComponent = "tMap_3";

				if (execStat) {
					runStat.updateStatOnConnection(resourceMap, iterateId, 0, 0, "row3");
				}

				int tos_count_tMap_3 = 0;

// ###############################
// # Lookup's keys initialization
// ###############################        

// ###############################
// # Vars initialization
				class Var__tMap_3__Struct {
				}
				Var__tMap_3__Struct Var__tMap_3 = new Var__tMap_3__Struct();
// ###############################

// ###############################
// # Outputs initialization
				out3Struct out3_tmp = new out3Struct();
// ###############################

				/**
				 * [tMap_3 begin ] stop
				 */

				/**
				 * [tUnite_1 begin ] start
				 */

				ok_Hash.put("tUnite_1", false);
				start_Hash.put("tUnite_1", System.currentTimeMillis());

				currentComponent = "tUnite_1";

				if (execStat) {
					runStat.updateStatOnConnection(resourceMap, iterateId, 0, 0, "out2", "out1");
				}

				int tos_count_tUnite_1 = 0;

				int nb_line_tUnite_1 = 0;

				/**
				 * [tUnite_1 begin ] stop
				 */

				/**
				 * [tMap_1 begin ] start
				 */

				ok_Hash.put("tMap_1", false);
				start_Hash.put("tMap_1", System.currentTimeMillis());

				currentComponent = "tMap_1";

				if (execStat) {
					runStat.updateStatOnConnection(resourceMap, iterateId, 0, 0, "row1");
				}

				int tos_count_tMap_1 = 0;

// ###############################
// # Lookup's keys initialization
// ###############################        

// ###############################
// # Vars initialization
				class Var__tMap_1__Struct {
				}
				Var__tMap_1__Struct Var__tMap_1 = new Var__tMap_1__Struct();
// ###############################

// ###############################
// # Outputs initialization
				out1Struct out1_tmp = new out1Struct();
// ###############################

				/**
				 * [tMap_1 begin ] stop
				 */

				/**
				 * [tFileInputDelimited_1 begin ] start
				 */

				ok_Hash.put("tFileInputDelimited_1", false);
				start_Hash.put("tFileInputDelimited_1", System.currentTimeMillis());

				currentComponent = "tFileInputDelimited_1";

				int tos_count_tFileInputDelimited_1 = 0;

				final routines.system.RowState rowstate_tFileInputDelimited_1 = new routines.system.RowState();

				int nb_line_tFileInputDelimited_1 = 0;
				org.talend.fileprocess.FileInputDelimited fid_tFileInputDelimited_1 = null;
				int limit_tFileInputDelimited_1 = -1;
				try {

					Object filename_tFileInputDelimited_1 = "C:/Users/neebr/OneDrive/Bureau/generate_clothes_data/data.csv";
					if (filename_tFileInputDelimited_1 instanceof java.io.InputStream) {

						int footer_value_tFileInputDelimited_1 = 0, random_value_tFileInputDelimited_1 = -1;
						if (footer_value_tFileInputDelimited_1 > 0 || random_value_tFileInputDelimited_1 > 0) {
							throw new java.lang.Exception(
									"When the input source is a stream,footer and random shouldn't be bigger than 0.");
						}

					}
					try {
						fid_tFileInputDelimited_1 = new org.talend.fileprocess.FileInputDelimited(
								"C:/Users/neebr/OneDrive/Bureau/generate_clothes_data/data.csv", "UTF-8", ",", "\n",
								false, 1, 0, limit_tFileInputDelimited_1, -1, false);
					} catch (java.lang.Exception e) {
						globalMap.put("tFileInputDelimited_1_ERROR_MESSAGE", e.getMessage());

						System.err.println(e.getMessage());

					}

					while (fid_tFileInputDelimited_1 != null && fid_tFileInputDelimited_1.nextRecord()) {
						rowstate_tFileInputDelimited_1.reset();

						row1 = null;

						boolean whetherReject_tFileInputDelimited_1 = false;
						row1 = new row1Struct();
						try {

							int columnIndexWithD_tFileInputDelimited_1 = 0;

							String temp = "";

							columnIndexWithD_tFileInputDelimited_1 = 0;

							row1.Type = fid_tFileInputDelimited_1.get(columnIndexWithD_tFileInputDelimited_1);

							columnIndexWithD_tFileInputDelimited_1 = 1;

							row1.Marque = fid_tFileInputDelimited_1.get(columnIndexWithD_tFileInputDelimited_1);

							columnIndexWithD_tFileInputDelimited_1 = 2;

							row1.Genre = fid_tFileInputDelimited_1.get(columnIndexWithD_tFileInputDelimited_1);

							columnIndexWithD_tFileInputDelimited_1 = 3;

							row1.Matiere = fid_tFileInputDelimited_1.get(columnIndexWithD_tFileInputDelimited_1);

							columnIndexWithD_tFileInputDelimited_1 = 4;

							row1.Taille = fid_tFileInputDelimited_1.get(columnIndexWithD_tFileInputDelimited_1);

							columnIndexWithD_tFileInputDelimited_1 = 5;

							row1.Saison = fid_tFileInputDelimited_1.get(columnIndexWithD_tFileInputDelimited_1);

							columnIndexWithD_tFileInputDelimited_1 = 6;

							row1.Couleur = fid_tFileInputDelimited_1.get(columnIndexWithD_tFileInputDelimited_1);

							columnIndexWithD_tFileInputDelimited_1 = 7;

							row1.Collection = fid_tFileInputDelimited_1.get(columnIndexWithD_tFileInputDelimited_1);

							columnIndexWithD_tFileInputDelimited_1 = 8;

							row1.Popularite = fid_tFileInputDelimited_1.get(columnIndexWithD_tFileInputDelimited_1);

							columnIndexWithD_tFileInputDelimited_1 = 9;

							temp = fid_tFileInputDelimited_1.get(columnIndexWithD_tFileInputDelimited_1);
							if (temp.length() > 0) {

								try {

									row1.Prix_origine = ParserUtils.parseTo_Float(temp);

								} catch (java.lang.Exception ex_tFileInputDelimited_1) {
									globalMap.put("tFileInputDelimited_1_ERROR_MESSAGE",
											ex_tFileInputDelimited_1.getMessage());
									rowstate_tFileInputDelimited_1.setException(new RuntimeException(String.format(
											"Couldn't parse value for column '%s' in '%s', value is '%s'. Details: %s",
											"Prix_origine", "row1", temp, ex_tFileInputDelimited_1),
											ex_tFileInputDelimited_1));
								}

							} else {

								row1.Prix_origine = null;

							}

							if (rowstate_tFileInputDelimited_1.getException() != null) {
								throw rowstate_tFileInputDelimited_1.getException();
							}

						} catch (java.lang.Exception e) {
							globalMap.put("tFileInputDelimited_1_ERROR_MESSAGE", e.getMessage());
							whetherReject_tFileInputDelimited_1 = true;

							System.err.println(e.getMessage());
							row1 = null;

						}

						/**
						 * [tFileInputDelimited_1 begin ] stop
						 */

						/**
						 * [tFileInputDelimited_1 main ] start
						 */

						currentComponent = "tFileInputDelimited_1";

						tos_count_tFileInputDelimited_1++;

						/**
						 * [tFileInputDelimited_1 main ] stop
						 */

						/**
						 * [tFileInputDelimited_1 process_data_begin ] start
						 */

						currentComponent = "tFileInputDelimited_1";

						/**
						 * [tFileInputDelimited_1 process_data_begin ] stop
						 */
// Start of branch "row1"
						if (row1 != null) {

							/**
							 * [tMap_1 main ] start
							 */

							currentComponent = "tMap_1";

							if (execStat) {
								runStat.updateStatOnConnection(iterateId, 1, 1

										, "row1"

								);
							}

							boolean hasCasePrimitiveKeyWithNull_tMap_1 = false;

							// ###############################
							// # Input tables (lookups)
							boolean rejectedInnerJoin_tMap_1 = false;
							boolean mainRowRejected_tMap_1 = false;

							// ###############################
							{ // start of Var scope

								// ###############################
								// # Vars tables

								Var__tMap_1__Struct Var = Var__tMap_1;// ###############################
								// ###############################
								// # Output tables

								out1 = null;

// # Output table : 'out1'
								out1_tmp.Marque = row1.Marque;
								out1_tmp.Type = row1.Type;
								out1_tmp.Couleur = row1.Couleur;
								out1_tmp.Taille = row1.Taille;
								out1_tmp.Matiere = row1.Matiere;
								out1_tmp.Prix_origine = row1.Prix_origine;
								out1_tmp.Genre = row1.Genre;
								out1_tmp.Saison = row1.Saison;
								out1_tmp.Collection = row1.Collection;
								out1_tmp.Popularite = row1.Popularite;
								out1 = out1_tmp;
// ###############################

							} // end of Var scope

							rejectedInnerJoin_tMap_1 = false;

							tos_count_tMap_1++;

							/**
							 * [tMap_1 main ] stop
							 */

							/**
							 * [tMap_1 process_data_begin ] start
							 */

							currentComponent = "tMap_1";

							/**
							 * [tMap_1 process_data_begin ] stop
							 */
// Start of branch "out1"
							if (out1 != null) {

								/**
								 * [tUnite_1 main ] start
								 */

								currentComponent = "tUnite_1";

								if (execStat) {
									runStat.updateStatOnConnection(iterateId, 1, 1

											, "out1"

									);
								}

//////////

// for output
								row3 = new row3Struct();

								row3.Marque = out1.Marque;
								row3.Type = out1.Type;
								row3.Couleur = out1.Couleur;
								row3.Taille = out1.Taille;
								row3.Matiere = out1.Matiere;
								row3.Prix_origine = out1.Prix_origine;
								row3.Genre = out1.Genre;
								row3.Saison = out1.Saison;
								row3.Collection = out1.Collection;
								row3.Popularite = out1.Popularite;

								nb_line_tUnite_1++;

//////////

								tos_count_tUnite_1++;

								/**
								 * [tUnite_1 main ] stop
								 */

								/**
								 * [tUnite_1 process_data_begin ] start
								 */

								currentComponent = "tUnite_1";

								/**
								 * [tUnite_1 process_data_begin ] stop
								 */

								/**
								 * [tMap_3 main ] start
								 */

								currentComponent = "tMap_3";

								if (execStat) {
									runStat.updateStatOnConnection(iterateId, 1, 1

											, "row3"

									);
								}

								boolean hasCasePrimitiveKeyWithNull_tMap_3 = false;

								// ###############################
								// # Input tables (lookups)
								boolean rejectedInnerJoin_tMap_3 = false;
								boolean mainRowRejected_tMap_3 = false;

								// ###############################
								{ // start of Var scope

									// ###############################
									// # Vars tables

									Var__tMap_3__Struct Var = Var__tMap_3;// ###############################
									// ###############################
									// # Output tables

									out3 = null;

// # Output table : 'out3'
									out3_tmp.Marque = row3.Marque;
									out3_tmp.Type = row3.Type;
									out3_tmp.Couleur = row3.Couleur;
									out3_tmp.Taille = row3.Taille;
									out3_tmp.Matiere = row3.Matiere;
									out3_tmp.Prix_origine = row3.Prix_origine;
									out3_tmp.Genre = row3.Genre;
									out3_tmp.Saison = row3.Saison;
									out3_tmp.Collection = row3.Collection;
									out3_tmp.Popularite = row3.Popularite;
									out3 = out3_tmp;
// ###############################

								} // end of Var scope

								rejectedInnerJoin_tMap_3 = false;

								tos_count_tMap_3++;

								/**
								 * [tMap_3 main ] stop
								 */

								/**
								 * [tMap_3 process_data_begin ] start
								 */

								currentComponent = "tMap_3";

								/**
								 * [tMap_3 process_data_begin ] stop
								 */
// Start of branch "out3"
								if (out3 != null) {

									/**
									 * [tDBOutput_1 main ] start
									 */

									currentComponent = "tDBOutput_1";

									if (execStat) {
										runStat.updateStatOnConnection(iterateId, 1, 1

												, "out3"

										);
									}

									whetherReject_tDBOutput_1 = false;
									if (out3.Marque == null) {
										pstmt_tDBOutput_1.setNull(1, java.sql.Types.VARCHAR);
									} else {
										pstmt_tDBOutput_1.setString(1, out3.Marque);
									}

									if (out3.Type == null) {
										pstmt_tDBOutput_1.setNull(2, java.sql.Types.VARCHAR);
									} else {
										pstmt_tDBOutput_1.setString(2, out3.Type);
									}

									if (out3.Couleur == null) {
										pstmt_tDBOutput_1.setNull(3, java.sql.Types.VARCHAR);
									} else {
										pstmt_tDBOutput_1.setString(3, out3.Couleur);
									}

									if (out3.Taille == null) {
										pstmt_tDBOutput_1.setNull(4, java.sql.Types.VARCHAR);
									} else {
										pstmt_tDBOutput_1.setString(4, out3.Taille);
									}

									if (out3.Matiere == null) {
										pstmt_tDBOutput_1.setNull(5, java.sql.Types.VARCHAR);
									} else {
										pstmt_tDBOutput_1.setString(5, out3.Matiere);
									}

									if (out3.Prix_origine == null) {
										pstmt_tDBOutput_1.setNull(6, java.sql.Types.FLOAT);
									} else {
										pstmt_tDBOutput_1.setFloat(6, out3.Prix_origine);
									}

									if (out3.Genre == null) {
										pstmt_tDBOutput_1.setNull(7, java.sql.Types.VARCHAR);
									} else {
										pstmt_tDBOutput_1.setString(7, out3.Genre);
									}

									if (out3.Saison == null) {
										pstmt_tDBOutput_1.setNull(8, java.sql.Types.VARCHAR);
									} else {
										pstmt_tDBOutput_1.setString(8, out3.Saison);
									}

									if (out3.Collection == null) {
										pstmt_tDBOutput_1.setNull(9, java.sql.Types.VARCHAR);
									} else {
										pstmt_tDBOutput_1.setString(9, out3.Collection);
									}

									if (out3.Popularite == null) {
										pstmt_tDBOutput_1.setNull(10, java.sql.Types.VARCHAR);
									} else {
										pstmt_tDBOutput_1.setString(10, out3.Popularite);
									}

									pstmt_tDBOutput_1.addBatch();
									nb_line_tDBOutput_1++;

									batchSizeCounter_tDBOutput_1++;

									if ((batchSize_tDBOutput_1 > 0)
											&& (batchSize_tDBOutput_1 <= batchSizeCounter_tDBOutput_1)) {
										try {
											int countSum_tDBOutput_1 = 0;

											for (int countEach_tDBOutput_1 : pstmt_tDBOutput_1.executeBatch()) {
												countSum_tDBOutput_1 += (countEach_tDBOutput_1 < 0 ? 0
														: countEach_tDBOutput_1);
											}
											rowsToCommitCount_tDBOutput_1 += countSum_tDBOutput_1;

											insertedCount_tDBOutput_1 += countSum_tDBOutput_1;

											batchSizeCounter_tDBOutput_1 = 0;
										} catch (java.sql.BatchUpdateException e_tDBOutput_1) {
											globalMap.put("tDBOutput_1_ERROR_MESSAGE", e_tDBOutput_1.getMessage());
											java.sql.SQLException ne_tDBOutput_1 = e_tDBOutput_1.getNextException(),
													sqle_tDBOutput_1 = null;
											String errormessage_tDBOutput_1;
											if (ne_tDBOutput_1 != null) {
												// build new exception to provide the original cause
												sqle_tDBOutput_1 = new java.sql.SQLException(
														e_tDBOutput_1.getMessage() + "\ncaused by: "
																+ ne_tDBOutput_1.getMessage(),
														ne_tDBOutput_1.getSQLState(), ne_tDBOutput_1.getErrorCode(),
														ne_tDBOutput_1);
												errormessage_tDBOutput_1 = sqle_tDBOutput_1.getMessage();
											} else {
												errormessage_tDBOutput_1 = e_tDBOutput_1.getMessage();
											}

											int countSum_tDBOutput_1 = 0;
											for (int countEach_tDBOutput_1 : e_tDBOutput_1.getUpdateCounts()) {
												countSum_tDBOutput_1 += (countEach_tDBOutput_1 < 0 ? 0
														: countEach_tDBOutput_1);
											}
											rowsToCommitCount_tDBOutput_1 += countSum_tDBOutput_1;

											insertedCount_tDBOutput_1 += countSum_tDBOutput_1;

											System.err.println(errormessage_tDBOutput_1);

										}
									}

									commitCounter_tDBOutput_1++;
									if (commitEvery_tDBOutput_1 <= commitCounter_tDBOutput_1) {
										if ((batchSize_tDBOutput_1 > 0) && (batchSizeCounter_tDBOutput_1 > 0)) {
											try {
												int countSum_tDBOutput_1 = 0;

												for (int countEach_tDBOutput_1 : pstmt_tDBOutput_1.executeBatch()) {
													countSum_tDBOutput_1 += (countEach_tDBOutput_1 < 0 ? 0
															: countEach_tDBOutput_1);
												}
												rowsToCommitCount_tDBOutput_1 += countSum_tDBOutput_1;

												insertedCount_tDBOutput_1 += countSum_tDBOutput_1;

												batchSizeCounter_tDBOutput_1 = 0;
											} catch (java.sql.BatchUpdateException e_tDBOutput_1) {
												globalMap.put("tDBOutput_1_ERROR_MESSAGE", e_tDBOutput_1.getMessage());
												java.sql.SQLException ne_tDBOutput_1 = e_tDBOutput_1.getNextException(),
														sqle_tDBOutput_1 = null;
												String errormessage_tDBOutput_1;
												if (ne_tDBOutput_1 != null) {
													// build new exception to provide the original cause
													sqle_tDBOutput_1 = new java.sql.SQLException(
															e_tDBOutput_1.getMessage() + "\ncaused by: "
																	+ ne_tDBOutput_1.getMessage(),
															ne_tDBOutput_1.getSQLState(), ne_tDBOutput_1.getErrorCode(),
															ne_tDBOutput_1);
													errormessage_tDBOutput_1 = sqle_tDBOutput_1.getMessage();
												} else {
													errormessage_tDBOutput_1 = e_tDBOutput_1.getMessage();
												}

												int countSum_tDBOutput_1 = 0;
												for (int countEach_tDBOutput_1 : e_tDBOutput_1.getUpdateCounts()) {
													countSum_tDBOutput_1 += (countEach_tDBOutput_1 < 0 ? 0
															: countEach_tDBOutput_1);
												}
												rowsToCommitCount_tDBOutput_1 += countSum_tDBOutput_1;

												insertedCount_tDBOutput_1 += countSum_tDBOutput_1;

												System.err.println(errormessage_tDBOutput_1);

											}
										}
										if (rowsToCommitCount_tDBOutput_1 != 0) {

										}
										conn_tDBOutput_1.commit();
										if (rowsToCommitCount_tDBOutput_1 != 0) {

											rowsToCommitCount_tDBOutput_1 = 0;
										}
										commitCounter_tDBOutput_1 = 0;
									}

									tos_count_tDBOutput_1++;

									/**
									 * [tDBOutput_1 main ] stop
									 */

									/**
									 * [tDBOutput_1 process_data_begin ] start
									 */

									currentComponent = "tDBOutput_1";

									/**
									 * [tDBOutput_1 process_data_begin ] stop
									 */

									/**
									 * [tDBOutput_1 process_data_end ] start
									 */

									currentComponent = "tDBOutput_1";

									/**
									 * [tDBOutput_1 process_data_end ] stop
									 */

								} // End of branch "out3"

								/**
								 * [tMap_3 process_data_end ] start
								 */

								currentComponent = "tMap_3";

								/**
								 * [tMap_3 process_data_end ] stop
								 */

								/**
								 * [tUnite_1 process_data_end ] start
								 */

								currentComponent = "tUnite_1";

								/**
								 * [tUnite_1 process_data_end ] stop
								 */

							} // End of branch "out1"

							/**
							 * [tMap_1 process_data_end ] start
							 */

							currentComponent = "tMap_1";

							/**
							 * [tMap_1 process_data_end ] stop
							 */

						} // End of branch "row1"

						/**
						 * [tFileInputDelimited_1 process_data_end ] start
						 */

						currentComponent = "tFileInputDelimited_1";

						/**
						 * [tFileInputDelimited_1 process_data_end ] stop
						 */

						/**
						 * [tFileInputDelimited_1 end ] start
						 */

						currentComponent = "tFileInputDelimited_1";

					}
				} finally {
					if (!((Object) ("C:/Users/neebr/OneDrive/Bureau/generate_clothes_data/data.csv") instanceof java.io.InputStream)) {
						if (fid_tFileInputDelimited_1 != null) {
							fid_tFileInputDelimited_1.close();
						}
					}
					if (fid_tFileInputDelimited_1 != null) {
						globalMap.put("tFileInputDelimited_1_NB_LINE", fid_tFileInputDelimited_1.getRowNumber());

					}
				}

				ok_Hash.put("tFileInputDelimited_1", true);
				end_Hash.put("tFileInputDelimited_1", System.currentTimeMillis());

				/**
				 * [tFileInputDelimited_1 end ] stop
				 */

				/**
				 * [tMap_1 end ] start
				 */

				currentComponent = "tMap_1";

// ###############################
// # Lookup hashes releasing
// ###############################      

				if (execStat) {
					runStat.updateStat(resourceMap, iterateId, 2, 0, "row1");
				}

				ok_Hash.put("tMap_1", true);
				end_Hash.put("tMap_1", System.currentTimeMillis());

				/**
				 * [tMap_1 end ] stop
				 */

				/**
				 * [tMap_2 begin ] start
				 */

				ok_Hash.put("tMap_2", false);
				start_Hash.put("tMap_2", System.currentTimeMillis());

				currentComponent = "tMap_2";

				if (execStat) {
					runStat.updateStatOnConnection(resourceMap, iterateId, 0, 0, "row2");
				}

				int tos_count_tMap_2 = 0;

// ###############################
// # Lookup's keys initialization
// ###############################        

// ###############################
// # Vars initialization
				class Var__tMap_2__Struct {
				}
				Var__tMap_2__Struct Var__tMap_2 = new Var__tMap_2__Struct();
// ###############################

// ###############################
// # Outputs initialization
				out2Struct out2_tmp = new out2Struct();
// ###############################

				/**
				 * [tMap_2 begin ] stop
				 */

				/**
				 * [tFileInputExcel_1 begin ] start
				 */

				ok_Hash.put("tFileInputExcel_1", false);
				start_Hash.put("tFileInputExcel_1", System.currentTimeMillis());

				currentComponent = "tFileInputExcel_1";

				int tos_count_tFileInputExcel_1 = 0;

				class RegexUtil_tFileInputExcel_1 {

					public java.util.List<jxl.Sheet> getSheets(jxl.Workbook workbook, String oneSheetName,
							boolean useRegex) {

						java.util.List<jxl.Sheet> list = new java.util.ArrayList<jxl.Sheet>();

						if (useRegex) {// this part process the regex issue

							jxl.Sheet[] sheets = workbook.getSheets();
							java.util.regex.Pattern pattern = java.util.regex.Pattern.compile(oneSheetName);
							for (int i = 0; i < sheets.length; i++) {
								String sheetName = sheets[i].getName();
								java.util.regex.Matcher matcher = pattern.matcher(sheetName);
								if (matcher.matches()) {
									jxl.Sheet sheet = workbook.getSheet(sheetName);
									if (sheet != null) {
										list.add(sheet);
									}
								}
							}

						} else {
							jxl.Sheet sheet = workbook.getSheet(oneSheetName);
							if (sheet != null) {
								list.add(sheet);
							}

						}

						return list;
					}

					public java.util.List<jxl.Sheet> getSheets(jxl.Workbook workbook, int index, boolean useRegex) {
						java.util.List<jxl.Sheet> list = new java.util.ArrayList<jxl.Sheet>();
						jxl.Sheet sheet = workbook.getSheet(index);
						if (sheet != null) {
							list.add(sheet);
						}
						return list;
					}

				}

				RegexUtil_tFileInputExcel_1 regexUtil_tFileInputExcel_1 = new RegexUtil_tFileInputExcel_1();
				final jxl.WorkbookSettings workbookSettings_tFileInputExcel_1 = new jxl.WorkbookSettings();
				workbookSettings_tFileInputExcel_1.setDrawingsDisabled(true);
				workbookSettings_tFileInputExcel_1.setEncoding("UTF-8");

				Object source_tFileInputExcel_1 = "C:/Users/neebr/OneDrive/Bureau/clothes_price_prediction_data.xls";
				final jxl.Workbook workbook_tFileInputExcel_1;

				java.io.InputStream toClose_tFileInputExcel_1 = null;
				java.io.BufferedInputStream buffIStreamtFileInputExcel_1 = null;
				try {
					if (source_tFileInputExcel_1 instanceof java.io.InputStream) {
						toClose_tFileInputExcel_1 = (java.io.InputStream) source_tFileInputExcel_1;
						buffIStreamtFileInputExcel_1 = new java.io.BufferedInputStream(toClose_tFileInputExcel_1);
						workbook_tFileInputExcel_1 = jxl.Workbook.getWorkbook(buffIStreamtFileInputExcel_1,
								workbookSettings_tFileInputExcel_1);
					} else if (source_tFileInputExcel_1 instanceof String) {
						toClose_tFileInputExcel_1 = new java.io.FileInputStream(source_tFileInputExcel_1.toString());
						buffIStreamtFileInputExcel_1 = new java.io.BufferedInputStream(toClose_tFileInputExcel_1);
						workbook_tFileInputExcel_1 = jxl.Workbook.getWorkbook(buffIStreamtFileInputExcel_1,
								workbookSettings_tFileInputExcel_1);
					} else {
						workbook_tFileInputExcel_1 = null;
						throw new java.lang.Exception(
								"The data source should be specified as Inputstream or File Path!");
					}
				} finally {
					try {
						if (buffIStreamtFileInputExcel_1 != null) {
							buffIStreamtFileInputExcel_1.close();
						}
					} catch (Exception e) {
						globalMap.put("tFileInputExcel_1_ERROR_MESSAGE", e.getMessage());
					}
				}
				try {
					java.util.List<jxl.Sheet> sheetList_tFileInputExcel_1 = java.util.Arrays.<jxl.Sheet>asList(
							workbook_tFileInputExcel_1.getSheets());
					if (sheetList_tFileInputExcel_1.size() <= 0) {
						throw new RuntimeException("Special sheets not exist!");
					}

					java.util.List<jxl.Sheet> sheet_FilterNullList_tFileInputExcel_1 = new java.util.ArrayList<jxl.Sheet>();
					for (jxl.Sheet sheet_FilterNull_tFileInputExcel_1 : sheetList_tFileInputExcel_1) {
						if (sheet_FilterNull_tFileInputExcel_1.getRows() > 0) {
							sheet_FilterNullList_tFileInputExcel_1.add(sheet_FilterNull_tFileInputExcel_1);
						}
					}
					sheetList_tFileInputExcel_1 = sheet_FilterNullList_tFileInputExcel_1;
					if (sheetList_tFileInputExcel_1.size() > 0) {
						int nb_line_tFileInputExcel_1 = 0;

						int begin_line_tFileInputExcel_1 = 1;

						int footer_input_tFileInputExcel_1 = 0;

						int end_line_tFileInputExcel_1 = 0;
						for (jxl.Sheet sheet_tFileInputExcel_1 : sheetList_tFileInputExcel_1) {
							end_line_tFileInputExcel_1 += sheet_tFileInputExcel_1.getRows();
						}
						end_line_tFileInputExcel_1 -= footer_input_tFileInputExcel_1;
						int limit_tFileInputExcel_1 = -1;
						int start_column_tFileInputExcel_1 = 1 - 1;
						int end_column_tFileInputExcel_1 = sheetList_tFileInputExcel_1.get(0).getColumns();
						jxl.Cell[] row_tFileInputExcel_1 = null;
						jxl.Sheet sheet_tFileInputExcel_1 = sheetList_tFileInputExcel_1.get(0);
						int rowCount_tFileInputExcel_1 = 0;
						int sheetIndex_tFileInputExcel_1 = 0;
						int currentRows_tFileInputExcel_1 = sheetList_tFileInputExcel_1.get(0).getRows();

						// for the number format
						java.text.DecimalFormat df_tFileInputExcel_1 = new java.text.DecimalFormat(
								"#.####################################");
						char separatorChar_tFileInputExcel_1 = df_tFileInputExcel_1.getDecimalFormatSymbols()
								.getDecimalSeparator();

						for (int i_tFileInputExcel_1 = begin_line_tFileInputExcel_1; i_tFileInputExcel_1 < end_line_tFileInputExcel_1; i_tFileInputExcel_1++) {

							int emptyColumnCount_tFileInputExcel_1 = 0;

							if (limit_tFileInputExcel_1 != -1 && nb_line_tFileInputExcel_1 >= limit_tFileInputExcel_1) {
								break;
							}

							while (i_tFileInputExcel_1 >= rowCount_tFileInputExcel_1 + currentRows_tFileInputExcel_1) {
								rowCount_tFileInputExcel_1 += currentRows_tFileInputExcel_1;
								sheet_tFileInputExcel_1 = sheetList_tFileInputExcel_1
										.get(++sheetIndex_tFileInputExcel_1);
								currentRows_tFileInputExcel_1 = sheet_tFileInputExcel_1.getRows();
							}
							if (rowCount_tFileInputExcel_1 <= i_tFileInputExcel_1) {
								row_tFileInputExcel_1 = sheet_tFileInputExcel_1
										.getRow(i_tFileInputExcel_1 - rowCount_tFileInputExcel_1);
							}
							globalMap.put("tFileInputExcel_1_CURRENT_SHEET", sheet_tFileInputExcel_1.getName());
							row2 = null;
							int tempRowLength_tFileInputExcel_1 = 6;

							int columnIndex_tFileInputExcel_1 = 0;

//
//end%>

							String[] temp_row_tFileInputExcel_1 = new String[tempRowLength_tFileInputExcel_1];
							int actual_end_column_tFileInputExcel_1 = end_column_tFileInputExcel_1 > row_tFileInputExcel_1.length
									? row_tFileInputExcel_1.length
									: end_column_tFileInputExcel_1;

							java.util.TimeZone zone_tFileInputExcel_1 = java.util.TimeZone.getTimeZone("GMT");
							java.text.SimpleDateFormat sdf_tFileInputExcel_1 = new java.text.SimpleDateFormat(
									"dd-MM-yyyy");
							sdf_tFileInputExcel_1.setTimeZone(zone_tFileInputExcel_1);

							for (int i = 0; i < tempRowLength_tFileInputExcel_1; i++) {

								if (i + start_column_tFileInputExcel_1 < actual_end_column_tFileInputExcel_1) {

									jxl.Cell cell_tFileInputExcel_1 = row_tFileInputExcel_1[i
											+ start_column_tFileInputExcel_1];
									temp_row_tFileInputExcel_1[i] = cell_tFileInputExcel_1.getContents();

								} else {
									temp_row_tFileInputExcel_1[i] = "";
								}
							}

							boolean whetherReject_tFileInputExcel_1 = false;
							row2 = new row2Struct();
							int curColNum_tFileInputExcel_1 = -1;
							String curColName_tFileInputExcel_1 = "";
							try {
								columnIndex_tFileInputExcel_1 = 0;

								if (temp_row_tFileInputExcel_1[columnIndex_tFileInputExcel_1].length() > 0) {
									curColNum_tFileInputExcel_1 = columnIndex_tFileInputExcel_1
											+ start_column_tFileInputExcel_1 + 1;
									curColName_tFileInputExcel_1 = "Brand";
									row2.Brand = temp_row_tFileInputExcel_1[columnIndex_tFileInputExcel_1];
								} else {
									row2.Brand = null;
									emptyColumnCount_tFileInputExcel_1++;
								}
								columnIndex_tFileInputExcel_1 = 1;

								if (temp_row_tFileInputExcel_1[columnIndex_tFileInputExcel_1].length() > 0) {
									curColNum_tFileInputExcel_1 = columnIndex_tFileInputExcel_1
											+ start_column_tFileInputExcel_1 + 1;
									curColName_tFileInputExcel_1 = "Category";
									row2.Category = temp_row_tFileInputExcel_1[columnIndex_tFileInputExcel_1];
								} else {
									row2.Category = null;
									emptyColumnCount_tFileInputExcel_1++;
								}
								columnIndex_tFileInputExcel_1 = 2;

								if (temp_row_tFileInputExcel_1[columnIndex_tFileInputExcel_1].length() > 0) {
									curColNum_tFileInputExcel_1 = columnIndex_tFileInputExcel_1
											+ start_column_tFileInputExcel_1 + 1;
									curColName_tFileInputExcel_1 = "Color";
									row2.Color = temp_row_tFileInputExcel_1[columnIndex_tFileInputExcel_1];
								} else {
									row2.Color = null;
									emptyColumnCount_tFileInputExcel_1++;
								}
								columnIndex_tFileInputExcel_1 = 3;

								if (temp_row_tFileInputExcel_1[columnIndex_tFileInputExcel_1].length() > 0) {
									curColNum_tFileInputExcel_1 = columnIndex_tFileInputExcel_1
											+ start_column_tFileInputExcel_1 + 1;
									curColName_tFileInputExcel_1 = "Size";
									row2.Size = temp_row_tFileInputExcel_1[columnIndex_tFileInputExcel_1];
								} else {
									row2.Size = null;
									emptyColumnCount_tFileInputExcel_1++;
								}
								columnIndex_tFileInputExcel_1 = 4;

								if (temp_row_tFileInputExcel_1[columnIndex_tFileInputExcel_1].length() > 0) {
									curColNum_tFileInputExcel_1 = columnIndex_tFileInputExcel_1
											+ start_column_tFileInputExcel_1 + 1;
									curColName_tFileInputExcel_1 = "Material";
									row2.Material = temp_row_tFileInputExcel_1[columnIndex_tFileInputExcel_1];
								} else {
									row2.Material = null;
									emptyColumnCount_tFileInputExcel_1++;
								}
								columnIndex_tFileInputExcel_1 = 5;

								if (temp_row_tFileInputExcel_1[columnIndex_tFileInputExcel_1].length() > 0) {
									curColNum_tFileInputExcel_1 = columnIndex_tFileInputExcel_1
											+ start_column_tFileInputExcel_1 + 1;
									curColName_tFileInputExcel_1 = "Price";
									row2.Price = ParserUtils
											.parseTo_Integer(temp_row_tFileInputExcel_1[columnIndex_tFileInputExcel_1]);
								} else {
									row2.Price = null;
									emptyColumnCount_tFileInputExcel_1++;
								}

								nb_line_tFileInputExcel_1++;

							} catch (java.lang.Exception e) {
								globalMap.put("tFileInputExcel_1_ERROR_MESSAGE", e.getMessage());
								whetherReject_tFileInputExcel_1 = true;
								System.err.println(e.getMessage());
								row2 = null;
							}

							/**
							 * [tFileInputExcel_1 begin ] stop
							 */

							/**
							 * [tFileInputExcel_1 main ] start
							 */

							currentComponent = "tFileInputExcel_1";

							tos_count_tFileInputExcel_1++;

							/**
							 * [tFileInputExcel_1 main ] stop
							 */

							/**
							 * [tFileInputExcel_1 process_data_begin ] start
							 */

							currentComponent = "tFileInputExcel_1";

							/**
							 * [tFileInputExcel_1 process_data_begin ] stop
							 */
// Start of branch "row2"
							if (row2 != null) {

								/**
								 * [tMap_2 main ] start
								 */

								currentComponent = "tMap_2";

								if (execStat) {
									runStat.updateStatOnConnection(iterateId, 1, 1

											, "row2"

									);
								}

								boolean hasCasePrimitiveKeyWithNull_tMap_2 = false;

								// ###############################
								// # Input tables (lookups)
								boolean rejectedInnerJoin_tMap_2 = false;
								boolean mainRowRejected_tMap_2 = false;

								// ###############################
								{ // start of Var scope

									// ###############################
									// # Vars tables

									Var__tMap_2__Struct Var = Var__tMap_2;// ###############################
									// ###############################
									// # Output tables

									out2 = null;

// # Output table : 'out2'
									out2_tmp.Brand = row2.Brand;
									out2_tmp.Category = row2.Category;
									out2_tmp.Color = row2.Color;
									out2_tmp.Size = row2.Size;
									out2_tmp.Material = row2.Material;
									out2_tmp.Price = (row2.Price != null) ? (float) row2.Price : null;
									out2 = out2_tmp;
// ###############################

								} // end of Var scope

								rejectedInnerJoin_tMap_2 = false;

								tos_count_tMap_2++;

								/**
								 * [tMap_2 main ] stop
								 */

								/**
								 * [tMap_2 process_data_begin ] start
								 */

								currentComponent = "tMap_2";

								/**
								 * [tMap_2 process_data_begin ] stop
								 */
// Start of branch "out2"
								if (out2 != null) {

									/**
									 * [tUnite_1 main ] start
									 */

									currentComponent = "tUnite_1";

									if (execStat) {
										runStat.updateStatOnConnection(iterateId, 1, 1

												, "out2"

										);
									}

//////////

// for output
									row3 = new row3Struct();

									row3.Marque = out2.Brand;
									row3.Type = out2.Category;
									row3.Couleur = out2.Color;
									row3.Taille = out2.Size;
									row3.Matiere = out2.Material;
									row3.Prix_origine = out2.Price;

									nb_line_tUnite_1++;

//////////

									tos_count_tUnite_1++;

									/**
									 * [tUnite_1 main ] stop
									 */

									/**
									 * [tUnite_1 process_data_begin ] start
									 */

									currentComponent = "tUnite_1";

									/**
									 * [tUnite_1 process_data_begin ] stop
									 */

									/**
									 * [tMap_3 main ] start
									 */

									currentComponent = "tMap_3";

									if (execStat) {
										runStat.updateStatOnConnection(iterateId, 1, 1

												, "row3"

										);
									}

									boolean hasCasePrimitiveKeyWithNull_tMap_3 = false;

									// ###############################
									// # Input tables (lookups)
									boolean rejectedInnerJoin_tMap_3 = false;
									boolean mainRowRejected_tMap_3 = false;

									// ###############################
									{ // start of Var scope

										// ###############################
										// # Vars tables

										Var__tMap_3__Struct Var = Var__tMap_3;// ###############################
										// ###############################
										// # Output tables

										out3 = null;

// # Output table : 'out3'
										out3_tmp.Marque = row3.Marque;
										out3_tmp.Type = row3.Type;
										out3_tmp.Couleur = row3.Couleur;
										out3_tmp.Taille = row3.Taille;
										out3_tmp.Matiere = row3.Matiere;
										out3_tmp.Prix_origine = row3.Prix_origine;
										out3_tmp.Genre = row3.Genre;
										out3_tmp.Saison = row3.Saison;
										out3_tmp.Collection = row3.Collection;
										out3_tmp.Popularite = row3.Popularite;
										out3 = out3_tmp;
// ###############################

									} // end of Var scope

									rejectedInnerJoin_tMap_3 = false;

									tos_count_tMap_3++;

									/**
									 * [tMap_3 main ] stop
									 */

									/**
									 * [tMap_3 process_data_begin ] start
									 */

									currentComponent = "tMap_3";

									/**
									 * [tMap_3 process_data_begin ] stop
									 */
// Start of branch "out3"
									if (out3 != null) {

										/**
										 * [tDBOutput_1 main ] start
										 */

										currentComponent = "tDBOutput_1";

										if (execStat) {
											runStat.updateStatOnConnection(iterateId, 1, 1

													, "out3"

											);
										}

										whetherReject_tDBOutput_1 = false;
										if (out3.Marque == null) {
											pstmt_tDBOutput_1.setNull(1, java.sql.Types.VARCHAR);
										} else {
											pstmt_tDBOutput_1.setString(1, out3.Marque);
										}

										if (out3.Type == null) {
											pstmt_tDBOutput_1.setNull(2, java.sql.Types.VARCHAR);
										} else {
											pstmt_tDBOutput_1.setString(2, out3.Type);
										}

										if (out3.Couleur == null) {
											pstmt_tDBOutput_1.setNull(3, java.sql.Types.VARCHAR);
										} else {
											pstmt_tDBOutput_1.setString(3, out3.Couleur);
										}

										if (out3.Taille == null) {
											pstmt_tDBOutput_1.setNull(4, java.sql.Types.VARCHAR);
										} else {
											pstmt_tDBOutput_1.setString(4, out3.Taille);
										}

										if (out3.Matiere == null) {
											pstmt_tDBOutput_1.setNull(5, java.sql.Types.VARCHAR);
										} else {
											pstmt_tDBOutput_1.setString(5, out3.Matiere);
										}

										if (out3.Prix_origine == null) {
											pstmt_tDBOutput_1.setNull(6, java.sql.Types.FLOAT);
										} else {
											pstmt_tDBOutput_1.setFloat(6, out3.Prix_origine);
										}

										if (out3.Genre == null) {
											pstmt_tDBOutput_1.setNull(7, java.sql.Types.VARCHAR);
										} else {
											pstmt_tDBOutput_1.setString(7, out3.Genre);
										}

										if (out3.Saison == null) {
											pstmt_tDBOutput_1.setNull(8, java.sql.Types.VARCHAR);
										} else {
											pstmt_tDBOutput_1.setString(8, out3.Saison);
										}

										if (out3.Collection == null) {
											pstmt_tDBOutput_1.setNull(9, java.sql.Types.VARCHAR);
										} else {
											pstmt_tDBOutput_1.setString(9, out3.Collection);
										}

										if (out3.Popularite == null) {
											pstmt_tDBOutput_1.setNull(10, java.sql.Types.VARCHAR);
										} else {
											pstmt_tDBOutput_1.setString(10, out3.Popularite);
										}

										pstmt_tDBOutput_1.addBatch();
										nb_line_tDBOutput_1++;

										batchSizeCounter_tDBOutput_1++;

										if ((batchSize_tDBOutput_1 > 0)
												&& (batchSize_tDBOutput_1 <= batchSizeCounter_tDBOutput_1)) {
											try {
												int countSum_tDBOutput_1 = 0;

												for (int countEach_tDBOutput_1 : pstmt_tDBOutput_1.executeBatch()) {
													countSum_tDBOutput_1 += (countEach_tDBOutput_1 < 0 ? 0
															: countEach_tDBOutput_1);
												}
												rowsToCommitCount_tDBOutput_1 += countSum_tDBOutput_1;

												insertedCount_tDBOutput_1 += countSum_tDBOutput_1;

												batchSizeCounter_tDBOutput_1 = 0;
											} catch (java.sql.BatchUpdateException e_tDBOutput_1) {
												globalMap.put("tDBOutput_1_ERROR_MESSAGE", e_tDBOutput_1.getMessage());
												java.sql.SQLException ne_tDBOutput_1 = e_tDBOutput_1.getNextException(),
														sqle_tDBOutput_1 = null;
												String errormessage_tDBOutput_1;
												if (ne_tDBOutput_1 != null) {
													// build new exception to provide the original cause
													sqle_tDBOutput_1 = new java.sql.SQLException(
															e_tDBOutput_1.getMessage() + "\ncaused by: "
																	+ ne_tDBOutput_1.getMessage(),
															ne_tDBOutput_1.getSQLState(), ne_tDBOutput_1.getErrorCode(),
															ne_tDBOutput_1);
													errormessage_tDBOutput_1 = sqle_tDBOutput_1.getMessage();
												} else {
													errormessage_tDBOutput_1 = e_tDBOutput_1.getMessage();
												}

												int countSum_tDBOutput_1 = 0;
												for (int countEach_tDBOutput_1 : e_tDBOutput_1.getUpdateCounts()) {
													countSum_tDBOutput_1 += (countEach_tDBOutput_1 < 0 ? 0
															: countEach_tDBOutput_1);
												}
												rowsToCommitCount_tDBOutput_1 += countSum_tDBOutput_1;

												insertedCount_tDBOutput_1 += countSum_tDBOutput_1;

												System.err.println(errormessage_tDBOutput_1);

											}
										}

										commitCounter_tDBOutput_1++;
										if (commitEvery_tDBOutput_1 <= commitCounter_tDBOutput_1) {
											if ((batchSize_tDBOutput_1 > 0) && (batchSizeCounter_tDBOutput_1 > 0)) {
												try {
													int countSum_tDBOutput_1 = 0;

													for (int countEach_tDBOutput_1 : pstmt_tDBOutput_1.executeBatch()) {
														countSum_tDBOutput_1 += (countEach_tDBOutput_1 < 0 ? 0
																: countEach_tDBOutput_1);
													}
													rowsToCommitCount_tDBOutput_1 += countSum_tDBOutput_1;

													insertedCount_tDBOutput_1 += countSum_tDBOutput_1;

													batchSizeCounter_tDBOutput_1 = 0;
												} catch (java.sql.BatchUpdateException e_tDBOutput_1) {
													globalMap.put("tDBOutput_1_ERROR_MESSAGE",
															e_tDBOutput_1.getMessage());
													java.sql.SQLException ne_tDBOutput_1 = e_tDBOutput_1
															.getNextException(), sqle_tDBOutput_1 = null;
													String errormessage_tDBOutput_1;
													if (ne_tDBOutput_1 != null) {
														// build new exception to provide the original cause
														sqle_tDBOutput_1 = new java.sql.SQLException(
																e_tDBOutput_1.getMessage() + "\ncaused by: "
																		+ ne_tDBOutput_1.getMessage(),
																ne_tDBOutput_1.getSQLState(),
																ne_tDBOutput_1.getErrorCode(), ne_tDBOutput_1);
														errormessage_tDBOutput_1 = sqle_tDBOutput_1.getMessage();
													} else {
														errormessage_tDBOutput_1 = e_tDBOutput_1.getMessage();
													}

													int countSum_tDBOutput_1 = 0;
													for (int countEach_tDBOutput_1 : e_tDBOutput_1.getUpdateCounts()) {
														countSum_tDBOutput_1 += (countEach_tDBOutput_1 < 0 ? 0
																: countEach_tDBOutput_1);
													}
													rowsToCommitCount_tDBOutput_1 += countSum_tDBOutput_1;

													insertedCount_tDBOutput_1 += countSum_tDBOutput_1;

													System.err.println(errormessage_tDBOutput_1);

												}
											}
											if (rowsToCommitCount_tDBOutput_1 != 0) {

											}
											conn_tDBOutput_1.commit();
											if (rowsToCommitCount_tDBOutput_1 != 0) {

												rowsToCommitCount_tDBOutput_1 = 0;
											}
											commitCounter_tDBOutput_1 = 0;
										}

										tos_count_tDBOutput_1++;

										/**
										 * [tDBOutput_1 main ] stop
										 */

										/**
										 * [tDBOutput_1 process_data_begin ] start
										 */

										currentComponent = "tDBOutput_1";

										/**
										 * [tDBOutput_1 process_data_begin ] stop
										 */

										/**
										 * [tDBOutput_1 process_data_end ] start
										 */

										currentComponent = "tDBOutput_1";

										/**
										 * [tDBOutput_1 process_data_end ] stop
										 */

									} // End of branch "out3"

									/**
									 * [tMap_3 process_data_end ] start
									 */

									currentComponent = "tMap_3";

									/**
									 * [tMap_3 process_data_end ] stop
									 */

									/**
									 * [tUnite_1 process_data_end ] start
									 */

									currentComponent = "tUnite_1";

									/**
									 * [tUnite_1 process_data_end ] stop
									 */

								} // End of branch "out2"

								/**
								 * [tMap_2 process_data_end ] start
								 */

								currentComponent = "tMap_2";

								/**
								 * [tMap_2 process_data_end ] stop
								 */

							} // End of branch "row2"

							/**
							 * [tFileInputExcel_1 process_data_end ] start
							 */

							currentComponent = "tFileInputExcel_1";

							/**
							 * [tFileInputExcel_1 process_data_end ] stop
							 */

							/**
							 * [tFileInputExcel_1 end ] start
							 */

							currentComponent = "tFileInputExcel_1";

						}

						globalMap.put("tFileInputExcel_1_NB_LINE", nb_line_tFileInputExcel_1);

					}

				} finally {

					if (!(source_tFileInputExcel_1 instanceof java.io.InputStream)) {
						workbook_tFileInputExcel_1.close();
					}

				}

				ok_Hash.put("tFileInputExcel_1", true);
				end_Hash.put("tFileInputExcel_1", System.currentTimeMillis());

				/**
				 * [tFileInputExcel_1 end ] stop
				 */

				/**
				 * [tMap_2 end ] start
				 */

				currentComponent = "tMap_2";

// ###############################
// # Lookup hashes releasing
// ###############################      

				if (execStat) {
					runStat.updateStat(resourceMap, iterateId, 2, 0, "row2");
				}

				ok_Hash.put("tMap_2", true);
				end_Hash.put("tMap_2", System.currentTimeMillis());

				/**
				 * [tMap_2 end ] stop
				 */

				/**
				 * [tUnite_1 end ] start
				 */

				currentComponent = "tUnite_1";

				globalMap.put("tUnite_1_NB_LINE", nb_line_tUnite_1);
				if (execStat) {
					runStat.updateStat(resourceMap, iterateId, 2, 0, "out2", "out1");
				}

				ok_Hash.put("tUnite_1", true);
				end_Hash.put("tUnite_1", System.currentTimeMillis());

				/**
				 * [tUnite_1 end ] stop
				 */

				/**
				 * [tMap_3 end ] start
				 */

				currentComponent = "tMap_3";

// ###############################
// # Lookup hashes releasing
// ###############################      

				if (execStat) {
					runStat.updateStat(resourceMap, iterateId, 2, 0, "row3");
				}

				ok_Hash.put("tMap_3", true);
				end_Hash.put("tMap_3", System.currentTimeMillis());

				/**
				 * [tMap_3 end ] stop
				 */

				/**
				 * [tDBOutput_1 end ] start
				 */

				currentComponent = "tDBOutput_1";

				try {
					int countSum_tDBOutput_1 = 0;
					if (pstmt_tDBOutput_1 != null && batchSizeCounter_tDBOutput_1 > 0) {

						for (int countEach_tDBOutput_1 : pstmt_tDBOutput_1.executeBatch()) {
							countSum_tDBOutput_1 += (countEach_tDBOutput_1 < 0 ? 0 : countEach_tDBOutput_1);
						}
						rowsToCommitCount_tDBOutput_1 += countSum_tDBOutput_1;

					}

					insertedCount_tDBOutput_1 += countSum_tDBOutput_1;

				} catch (java.sql.BatchUpdateException e_tDBOutput_1) {
					globalMap.put("tDBOutput_1_ERROR_MESSAGE", e_tDBOutput_1.getMessage());
					java.sql.SQLException ne_tDBOutput_1 = e_tDBOutput_1.getNextException(), sqle_tDBOutput_1 = null;
					String errormessage_tDBOutput_1;
					if (ne_tDBOutput_1 != null) {
						// build new exception to provide the original cause
						sqle_tDBOutput_1 = new java.sql.SQLException(
								e_tDBOutput_1.getMessage() + "\ncaused by: " + ne_tDBOutput_1.getMessage(),
								ne_tDBOutput_1.getSQLState(), ne_tDBOutput_1.getErrorCode(), ne_tDBOutput_1);
						errormessage_tDBOutput_1 = sqle_tDBOutput_1.getMessage();
					} else {
						errormessage_tDBOutput_1 = e_tDBOutput_1.getMessage();
					}

					int countSum_tDBOutput_1 = 0;
					for (int countEach_tDBOutput_1 : e_tDBOutput_1.getUpdateCounts()) {
						countSum_tDBOutput_1 += (countEach_tDBOutput_1 < 0 ? 0 : countEach_tDBOutput_1);
					}
					rowsToCommitCount_tDBOutput_1 += countSum_tDBOutput_1;

					insertedCount_tDBOutput_1 += countSum_tDBOutput_1;

					System.err.println(errormessage_tDBOutput_1);

				}

				if (pstmt_tDBOutput_1 != null) {

					pstmt_tDBOutput_1.close();
					resourceMap.remove("pstmt_tDBOutput_1");
				}
				resourceMap.put("statementClosed_tDBOutput_1", true);
				if (rowsToCommitCount_tDBOutput_1 != 0) {

				}
				conn_tDBOutput_1.commit();
				if (rowsToCommitCount_tDBOutput_1 != 0) {

					rowsToCommitCount_tDBOutput_1 = 0;
				}
				commitCounter_tDBOutput_1 = 0;

				conn_tDBOutput_1.close();

				resourceMap.put("finish_tDBOutput_1", true);

				nb_line_deleted_tDBOutput_1 = nb_line_deleted_tDBOutput_1 + deletedCount_tDBOutput_1;
				nb_line_update_tDBOutput_1 = nb_line_update_tDBOutput_1 + updatedCount_tDBOutput_1;
				nb_line_inserted_tDBOutput_1 = nb_line_inserted_tDBOutput_1 + insertedCount_tDBOutput_1;
				nb_line_rejected_tDBOutput_1 = nb_line_rejected_tDBOutput_1 + rejectedCount_tDBOutput_1;

				globalMap.put("tDBOutput_1_NB_LINE", nb_line_tDBOutput_1);
				globalMap.put("tDBOutput_1_NB_LINE_UPDATED", nb_line_update_tDBOutput_1);
				globalMap.put("tDBOutput_1_NB_LINE_INSERTED", nb_line_inserted_tDBOutput_1);
				globalMap.put("tDBOutput_1_NB_LINE_DELETED", nb_line_deleted_tDBOutput_1);
				globalMap.put("tDBOutput_1_NB_LINE_REJECTED", nb_line_rejected_tDBOutput_1);

				if (execStat) {
					runStat.updateStat(resourceMap, iterateId, 2, 0, "out3");
				}

				ok_Hash.put("tDBOutput_1", true);
				end_Hash.put("tDBOutput_1", System.currentTimeMillis());

				/**
				 * [tDBOutput_1 end ] stop
				 */

			} // end the resume

		} catch (java.lang.Exception e) {

			TalendException te = new TalendException(e, currentComponent, globalMap);

			throw te;
		} catch (java.lang.Error error) {

			runStat.stopThreadStat();

			throw error;
		} finally {

			try {

				/**
				 * [tFileInputDelimited_1 finally ] start
				 */

				currentComponent = "tFileInputDelimited_1";

				/**
				 * [tFileInputDelimited_1 finally ] stop
				 */

				/**
				 * [tMap_1 finally ] start
				 */

				currentComponent = "tMap_1";

				/**
				 * [tMap_1 finally ] stop
				 */

				/**
				 * [tFileInputExcel_1 finally ] start
				 */

				currentComponent = "tFileInputExcel_1";

				/**
				 * [tFileInputExcel_1 finally ] stop
				 */

				/**
				 * [tMap_2 finally ] start
				 */

				currentComponent = "tMap_2";

				/**
				 * [tMap_2 finally ] stop
				 */

				/**
				 * [tUnite_1 finally ] start
				 */

				currentComponent = "tUnite_1";

				/**
				 * [tUnite_1 finally ] stop
				 */

				/**
				 * [tMap_3 finally ] start
				 */

				currentComponent = "tMap_3";

				/**
				 * [tMap_3 finally ] stop
				 */

				/**
				 * [tDBOutput_1 finally ] start
				 */

				currentComponent = "tDBOutput_1";

				try {
					if (resourceMap.get("statementClosed_tDBOutput_1") == null) {
						java.sql.PreparedStatement pstmtToClose_tDBOutput_1 = null;
						if ((pstmtToClose_tDBOutput_1 = (java.sql.PreparedStatement) resourceMap
								.remove("pstmt_tDBOutput_1")) != null) {
							pstmtToClose_tDBOutput_1.close();
						}
					}
				} finally {
					if (resourceMap.get("finish_tDBOutput_1") == null) {
						java.sql.Connection ctn_tDBOutput_1 = null;
						if ((ctn_tDBOutput_1 = (java.sql.Connection) resourceMap.get("conn_tDBOutput_1")) != null) {
							try {
								ctn_tDBOutput_1.close();
							} catch (java.sql.SQLException sqlEx_tDBOutput_1) {
								String errorMessage_tDBOutput_1 = "failed to close the connection in tDBOutput_1 :"
										+ sqlEx_tDBOutput_1.getMessage();
								System.err.println(errorMessage_tDBOutput_1);
							}
						}
					}
				}

				/**
				 * [tDBOutput_1 finally ] stop
				 */

			} catch (java.lang.Exception e) {
				// ignore
			} catch (java.lang.Error error) {
				// ignore
			}
			resourceMap = null;
		}

		globalMap.put("tFileInputDelimited_1_SUBPROCESS_STATE", 1);
	}

	public String resuming_logs_dir_path = null;
	public String resuming_checkpoint_path = null;
	public String parent_part_launcher = null;
	private String resumeEntryMethodName = null;
	private boolean globalResumeTicket = false;

	public boolean watch = false;
	// portStats is null, it means don't execute the statistics
	public Integer portStats = null;
	public int portTraces = 4334;
	public String clientHost;
	public String defaultClientHost = "localhost";
	public String contextStr = "Default";
	public boolean isDefaultContext = true;
	public String pid = "0";
	public String rootPid = null;
	public String fatherPid = null;
	public String fatherNode = null;
	public long startTime = 0;
	public boolean isChildJob = false;
	public String log4jLevel = "";

	private boolean enableLogStash;

	private boolean execStat = true;

	private ThreadLocal<java.util.Map<String, String>> threadLocal = new ThreadLocal<java.util.Map<String, String>>() {
		protected java.util.Map<String, String> initialValue() {
			java.util.Map<String, String> threadRunResultMap = new java.util.HashMap<String, String>();
			threadRunResultMap.put("errorCode", null);
			threadRunResultMap.put("status", "");
			return threadRunResultMap;
		};
	};

	protected PropertiesWithType context_param = new PropertiesWithType();
	public java.util.Map<String, Object> parentContextMap = new java.util.HashMap<String, Object>();

	public String status = "";

	public static void main(String[] args) {
		final unite_files unite_filesClass = new unite_files();

		int exitCode = unite_filesClass.runJobInTOS(args);

		System.exit(exitCode);
	}

	public String[][] runJob(String[] args) {

		int exitCode = runJobInTOS(args);
		String[][] bufferValue = new String[][] { { Integer.toString(exitCode) } };

		return bufferValue;
	}

	public boolean hastBufferOutputComponent() {
		boolean hastBufferOutput = false;

		return hastBufferOutput;
	}

	public int runJobInTOS(String[] args) {
		// reset status
		status = "";

		String lastStr = "";
		for (String arg : args) {
			if (arg.equalsIgnoreCase("--context_param")) {
				lastStr = arg;
			} else if (lastStr.equals("")) {
				evalParam(arg);
			} else {
				evalParam(lastStr + " " + arg);
				lastStr = "";
			}
		}
		enableLogStash = "true".equalsIgnoreCase(System.getProperty("audit.enabled"));

		if (clientHost == null) {
			clientHost = defaultClientHost;
		}

		if (pid == null || "0".equals(pid)) {
			pid = TalendString.getAsciiRandomString(6);
		}

		if (rootPid == null) {
			rootPid = pid;
		}
		if (fatherPid == null) {
			fatherPid = pid;
		} else {
			isChildJob = true;
		}

		if (portStats != null) {
			// portStats = -1; //for testing
			if (portStats < 0 || portStats > 65535) {
				// issue:10869, the portStats is invalid, so this client socket can't open
				System.err.println("The statistics socket port " + portStats + " is invalid.");
				execStat = false;
			}
		} else {
			execStat = false;
		}
		boolean inOSGi = routines.system.BundleUtils.inOSGi();

		if (inOSGi) {
			java.util.Dictionary<String, Object> jobProperties = routines.system.BundleUtils.getJobProperties(jobName);

			if (jobProperties != null && jobProperties.get("context") != null) {
				contextStr = (String) jobProperties.get("context");
			}
		}

		try {
			// call job/subjob with an existing context, like: --context=production. if
			// without this parameter, there will use the default context instead.
			java.io.InputStream inContext = unite_files.class.getClassLoader()
					.getResourceAsStream("restyle_up/unite_files_0_1/contexts/" + contextStr + ".properties");
			if (inContext == null) {
				inContext = unite_files.class.getClassLoader()
						.getResourceAsStream("config/contexts/" + contextStr + ".properties");
			}
			if (inContext != null) {
				try {
					// defaultProps is in order to keep the original context value
					if (context != null && context.isEmpty()) {
						defaultProps.load(inContext);
						context = new ContextProperties(defaultProps);
					}
				} finally {
					inContext.close();
				}
			} else if (!isDefaultContext) {
				// print info and job continue to run, for case: context_param is not empty.
				System.err.println("Could not find the context " + contextStr);
			}

			if (!context_param.isEmpty()) {
				context.putAll(context_param);
				// set types for params from parentJobs
				for (Object key : context_param.keySet()) {
					String context_key = key.toString();
					String context_type = context_param.getContextType(context_key);
					context.setContextType(context_key, context_type);

				}
			}
			class ContextProcessing {
				private void processContext_0() {
				}

				public void processAllContext() {
					processContext_0();
				}
			}

			new ContextProcessing().processAllContext();
		} catch (java.io.IOException ie) {
			System.err.println("Could not load context " + contextStr);
			ie.printStackTrace();
		}

		// get context value from parent directly
		if (parentContextMap != null && !parentContextMap.isEmpty()) {
		}

		// Resume: init the resumeUtil
		resumeEntryMethodName = ResumeUtil.getResumeEntryMethodName(resuming_checkpoint_path);
		resumeUtil = new ResumeUtil(resuming_logs_dir_path, isChildJob, rootPid);
		resumeUtil.initCommonInfo(pid, rootPid, fatherPid, projectName, jobName, contextStr, jobVersion);

		List<String> parametersToEncrypt = new java.util.ArrayList<String>();
		// Resume: jobStart
		resumeUtil.addLog("JOB_STARTED", "JOB:" + jobName, parent_part_launcher, Thread.currentThread().getId() + "",
				"", "", "", "", resumeUtil.convertToJsonText(context, parametersToEncrypt));

		if (execStat) {
			try {
				runStat.openSocket(!isChildJob);
				runStat.setAllPID(rootPid, fatherPid, pid, jobName);
				runStat.startThreadStat(clientHost, portStats);
				runStat.updateStatOnJob(RunStat.JOBSTART, fatherNode);
			} catch (java.io.IOException ioException) {
				ioException.printStackTrace();
			}
		}

		java.util.concurrent.ConcurrentHashMap<Object, Object> concurrentHashMap = new java.util.concurrent.ConcurrentHashMap<Object, Object>();
		globalMap.put("concurrentHashMap", concurrentHashMap);

		long startUsedMemory = Runtime.getRuntime().totalMemory() - Runtime.getRuntime().freeMemory();
		long endUsedMemory = 0;
		long end = 0;

		startTime = System.currentTimeMillis();

		this.globalResumeTicket = true;// to run tPreJob

		this.globalResumeTicket = false;// to run others jobs

		try {
			errorCode = null;
			tFileInputDelimited_1Process(globalMap);
			if (!"failure".equals(status)) {
				status = "end";
			}
		} catch (TalendException e_tFileInputDelimited_1) {
			globalMap.put("tFileInputDelimited_1_SUBPROCESS_STATE", -1);

			e_tFileInputDelimited_1.printStackTrace();

		}

		this.globalResumeTicket = true;// to run tPostJob

		end = System.currentTimeMillis();

		if (watch) {
			System.out.println((end - startTime) + " milliseconds");
		}

		endUsedMemory = Runtime.getRuntime().totalMemory() - Runtime.getRuntime().freeMemory();
		if (false) {
			System.out.println((endUsedMemory - startUsedMemory) + " bytes memory increase when running : unite_files");
		}

		if (execStat) {
			runStat.updateStatOnJob(RunStat.JOBEND, fatherNode);
			runStat.stopThreadStat();
		}
		int returnCode = 0;

		if (errorCode == null) {
			returnCode = status != null && status.equals("failure") ? 1 : 0;
		} else {
			returnCode = errorCode.intValue();
		}
		resumeUtil.addLog("JOB_ENDED", "JOB:" + jobName, parent_part_launcher, Thread.currentThread().getId() + "", "",
				"" + returnCode, "", "", "");

		return returnCode;

	}

	// only for OSGi env
	public void destroy() {

	}

	private java.util.Map<String, Object> getSharedConnections4REST() {
		java.util.Map<String, Object> connections = new java.util.HashMap<String, Object>();

		return connections;
	}

	private void evalParam(String arg) {
		if (arg.startsWith("--resuming_logs_dir_path")) {
			resuming_logs_dir_path = arg.substring(25);
		} else if (arg.startsWith("--resuming_checkpoint_path")) {
			resuming_checkpoint_path = arg.substring(27);
		} else if (arg.startsWith("--parent_part_launcher")) {
			parent_part_launcher = arg.substring(23);
		} else if (arg.startsWith("--watch")) {
			watch = true;
		} else if (arg.startsWith("--stat_port=")) {
			String portStatsStr = arg.substring(12);
			if (portStatsStr != null && !portStatsStr.equals("null")) {
				portStats = Integer.parseInt(portStatsStr);
			}
		} else if (arg.startsWith("--trace_port=")) {
			portTraces = Integer.parseInt(arg.substring(13));
		} else if (arg.startsWith("--client_host=")) {
			clientHost = arg.substring(14);
		} else if (arg.startsWith("--context=")) {
			contextStr = arg.substring(10);
			isDefaultContext = false;
		} else if (arg.startsWith("--father_pid=")) {
			fatherPid = arg.substring(13);
		} else if (arg.startsWith("--root_pid=")) {
			rootPid = arg.substring(11);
		} else if (arg.startsWith("--father_node=")) {
			fatherNode = arg.substring(14);
		} else if (arg.startsWith("--pid=")) {
			pid = arg.substring(6);
		} else if (arg.startsWith("--context_type")) {
			String keyValue = arg.substring(15);
			int index = -1;
			if (keyValue != null && (index = keyValue.indexOf('=')) > -1) {
				if (fatherPid == null) {
					context_param.setContextType(keyValue.substring(0, index),
							replaceEscapeChars(keyValue.substring(index + 1)));
				} else { // the subjob won't escape the especial chars
					context_param.setContextType(keyValue.substring(0, index), keyValue.substring(index + 1));
				}

			}

		} else if (arg.startsWith("--context_param")) {
			String keyValue = arg.substring(16);
			int index = -1;
			if (keyValue != null && (index = keyValue.indexOf('=')) > -1) {
				if (fatherPid == null) {
					context_param.put(keyValue.substring(0, index), replaceEscapeChars(keyValue.substring(index + 1)));
				} else { // the subjob won't escape the especial chars
					context_param.put(keyValue.substring(0, index), keyValue.substring(index + 1));
				}
			}
		} else if (arg.startsWith("--log4jLevel=")) {
			log4jLevel = arg.substring(13);
		} else if (arg.startsWith("--audit.enabled") && arg.contains("=")) {// for trunjob call
			final int equal = arg.indexOf('=');
			final String key = arg.substring("--".length(), equal);
			System.setProperty(key, arg.substring(equal + 1));
		}
	}

	private static final String NULL_VALUE_EXPRESSION_IN_COMMAND_STRING_FOR_CHILD_JOB_ONLY = "<TALEND_NULL>";

	private final String[][] escapeChars = { { "\\\\", "\\" }, { "\\n", "\n" }, { "\\'", "\'" }, { "\\r", "\r" },
			{ "\\f", "\f" }, { "\\b", "\b" }, { "\\t", "\t" } };

	private String replaceEscapeChars(String keyValue) {

		if (keyValue == null || ("").equals(keyValue.trim())) {
			return keyValue;
		}

		StringBuilder result = new StringBuilder();
		int currIndex = 0;
		while (currIndex < keyValue.length()) {
			int index = -1;
			// judege if the left string includes escape chars
			for (String[] strArray : escapeChars) {
				index = keyValue.indexOf(strArray[0], currIndex);
				if (index >= 0) {

					result.append(keyValue.substring(currIndex, index + strArray[0].length()).replace(strArray[0],
							strArray[1]));
					currIndex = index + strArray[0].length();
					break;
				}
			}
			// if the left string doesn't include escape chars, append the left into the
			// result
			if (index < 0) {
				result.append(keyValue.substring(currIndex));
				currIndex = currIndex + keyValue.length();
			}
		}

		return result.toString();
	}

	public Integer getErrorCode() {
		return errorCode;
	}

	public String getStatus() {
		return status;
	}

	ResumeUtil resumeUtil = null;
}
/************************************************************************************************
 * 134537 characters generated by Talend Open Studio for Data Integration on the
 * 10 avril 2025 à 10:31:03 AM CET
 ************************************************************************************************/