<template>
  <span>
    <Btn
      :id="`uploadBtn${_uid}`"
      :disabled="loading || disabled"
      primary
      fab
      class="relative"
      @click="!disabled && $refs.file.click()">
      <span
        v-show="loading"
        :style="{ width: `${Math.floor(loaded)}%` }"
        class="absolute fill bg-white op30" />
      <Icon
        name="upload" />
    </Btn>
    <input
      ref="file"
      type="file"
      style="position: fixed; top: -100em"
      @change="pick">
  </span>
</template>

<script>
  import axios from "axios";

  export default {
    name: "UploadButton",
    props: {
      value: {
        type: String,
        required: false,
        default: null,
      },
      prefix: {
        type: [String, Number],
        required: false,
        default: null,
      },
      sendLabel: {
        type: String,
        required: false,
        default: "Enviar",
      },
      sentLabel: {
        type: String,
        required: false,
        default: "OK",
      },
      acceptedTypes: {
        type: Array,
        default: () => ["application/pdf", "image/jpeg", "image/png"],
      },
      disabled: {
        type: Boolean,
        default: false,
      },
    },
    data() {
      return {
        loading: false,
        loaded: 0,
      };
    },
    methods: {
      async pick() {
        const file = this.$refs.file.files[0];
        if (!this.acceptedTypes.includes(file.type)) {
          this.notify("Tipo de arquivo não aceito.");
          return;
        }

        this.loading = true;
        this.loaded = 0;
        const fileName = `${this.prefix}${new Date().getTime()}-${file.name}`;
        await this.$store.dispatch("presign", fileName);
        const url = this.$store.state.uploadUrl;

        const headers = {
          "x-ms-blob-type": "BlockBlob",
          "x-ms-blob-content-type": file.type,
        };
        await axios.put(url, file, { headers, onUploadProgress: this.setProg });
        this.$emit("input", url.split("?")[0]);
        this.loading = false;
      },
      setProg(e) {
        const ratio = e.loaded / e.total;
        this.loaded = Math.floor(ratio * 100.0);
      },
    },
  };
</script>
